using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PeliApi.Servicios;
using PeliApi.Entidades;
using PeliApi.DTO_s;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.JsonPatch;
using PeliApi.Helpers;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;

namespace PeliApi.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly ILogger<PeliculasController> logger;
        private readonly string contenedor = "Peliculas";

        public PeliculasController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos,
            ILogger<PeliculasController> logger)  
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<PeliculasIndexDTO> Get()
        {

            var top = 5;
            var hoy = DateTime.Today;

            var proximosEstrenos = await context.Peliculas
                .Where(x => x.FechaEstreno > hoy)
                .OrderBy(x => x.FechaEstreno)
                .Take(top)
                .ToListAsync();

            var enCine = await context.Peliculas
                .Where(x => x.EnCines)
                .Take(top)
                .ToListAsync();

            var resultado = new PeliculasIndexDTO();

            resultado.FuturosEstrenos = mapper.Map<List<PeliculaDTO>>(proximosEstrenos);
            resultado.EnCines = mapper.Map<List<PeliculaDTO>>(enCine);

            return resultado;

          // var peliculas = await context.Peliculas.ToListAsync();
          //  return mapper.Map<List<PeliculaDTO>>(peliculas);
        }



        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO)
        {
            var peliculasQueryable = context.Peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(filtroPeliculasDTO.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.Contains(filtroPeliculasDTO.Titulo));
            }

            if (filtroPeliculasDTO.EnCines)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.EnCines);
            }

            if (filtroPeliculasDTO.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(x => x.FechaEstreno > hoy);
            }

            if (filtroPeliculasDTO.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.PeliculasGeneros.Select(y => y.GeneroId).Contains(filtroPeliculasDTO.GeneroId));
            }

            if (!string.IsNullOrEmpty(filtroPeliculasDTO.campoOrdenar))
            {
                //if (filtroPeliculasDTO.campoOrdenar == "titulo")
                //{

                //    if (filtroPeliculasDTO.OrdenAscendente)
                //    {
                //        peliculasQueryable = peliculasQueryable.OrderBy(x => x.Titulo);
                //    }
                //    else
                //    {
                //        peliculasQueryable = peliculasQueryable.OrderByDescending(x => x.Titulo);
                //    }

                //}

                var tipoOrden = filtroPeliculasDTO.OrdenAscendente ? "ascending" : "descending";

                try
                {
                    peliculasQueryable = peliculasQueryable.OrderBy($"{filtroPeliculasDTO.campoOrdenar} {tipoOrden}");
                }
                catch (Exception ex)
                {

                    logger.LogError(ex.Message, ex);
                }
                



            }

            await HttpContext.InsertarParametroPaginacion(peliculasQueryable, filtroPeliculasDTO.CantidadRegistroPorPagina);

            var peliculas = await peliculasQueryable.Paginar(filtroPeliculasDTO.paginacion).ToListAsync();

            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }


        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculasDetalleDTO>> Get(int id) {

            var peliculas = await context.Peliculas.
                Include(x => x.PeliculasActores).
                ThenInclude(x => x.Actor).
                Include(x => x.PeliculasGeneros).
                
                FirstOrDefaultAsync(x => x.Id == id);
            if (peliculas == null)
            {
                return NotFound();
            }


            peliculas.PeliculasActores = peliculas.PeliculasActores.OrderBy(x => x.Orden).ToList();



            var dtos = mapper.Map<PeliculasDetalleDTO>(peliculas);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var entidades = mapper.Map<Peliculas>(peliculaCreacionDTO);


            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    entidades.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, peliculaCreacionDTO.Poster.ContentType);
                }
            }


            AsignarOrdenActores(entidades);
            context.Add(entidades);
            await context.SaveChangesAsync();
            var Peliculadto = mapper.Map<PeliculaDTO>(entidades);

            return new CreatedAtRouteResult("obtenerPelicula", new { id = Peliculadto.Id }, Peliculadto);

        }


        private void AsignarOrdenActores(Peliculas peliculas)
        {
            if (peliculas.PeliculasActores != null)
            {
                for (int i = 0; i < peliculas.PeliculasActores.Count; i++)
                {
                    peliculas.PeliculasActores[i].Orden = i;
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            var peliculas = await context.Peliculas
                .Include(x  => x.PeliculasActores)
                .Include(x => x.PeliculasGeneros)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (peliculas == null)
            {
                return NotFound();
            }

            peliculas = mapper.Map(peliculaCreacionDTO, peliculas);

            if (peliculaCreacionDTO.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await peliculaCreacionDTO.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(peliculaCreacionDTO.Poster.FileName);
                    peliculas.Poster = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculaCreacionDTO.Poster.ContentType, peliculas.Poster);
                }
            }

            AsignarOrdenActores(peliculas);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> PatchDocument)
        {
            if (PatchDocument == null)
            {
                return BadRequest();
            }

            var peli = await context.Peliculas.FirstOrDefaultAsync(x => x.Id == id);

            if (peli == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<PeliculaPatchDTO>(peli);

            PatchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, peli);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Peliculas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Peliculas() { Id = id});
            return NoContent();
        }
    }
}
