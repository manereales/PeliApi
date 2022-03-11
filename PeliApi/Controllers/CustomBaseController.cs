using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliApi.DTO_s;
using PeliApi.Entidades;
using PeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Controllers
{


    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await context.Set<TEntidad>().AsNoTracking().ToListAsync();

            var dtos = mapper.Map<List<TDTO>>(entidades);

            return dtos;

        }


        protected async Task<List<TDTO>> Get<TEntidad, TDTO>(PaginacionDTO paginacionDTO) where TEntidad : class
        {
            var queryable = context.Set<TEntidad>().AsNoTracking().AsQueryable();
            await HttpContext.InsertarParametroPaginacion(queryable, paginacionDTO.CantidadRegistroPorPagina);
            var entidades = await queryable.Paginar(paginacionDTO).ToListAsync();
            var dtos = mapper.Map<List<TDTO>>(entidades);

            return dtos;
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidad = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(z => z.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return mapper.Map<TDTO>(entidad);

        }


        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>(TCreacion creacionDTO, string NombreRuta) where TEntidad : class, IId
        {

            var entidad = mapper.Map<TEntidad>(creacionDTO);

            context.Add(entidad);
            await context.SaveChangesAsync();

            var dtolectura = mapper.Map<TLectura>(entidad);

            return new CreatedAtRouteResult(NombreRuta, new { id = entidad.Id }, dtolectura);
        }

        protected async Task<ActionResult> Put<TCreacion, TEntidad>(TCreacion creacion, int id) where TEntidad : class, IId
        {
            var entidad = mapper.Map<TEntidad>(creacion);


            entidad.Id = id;

            context.Entry(entidad).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();

        }

        protected async Task<ActionResult> Patch<TCreacion, TEntidad>(int id, JsonPatchDocument<TCreacion> patchDocument) where TCreacion : class where TEntidad : class, IId
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entidadDb = await context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDb == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<TCreacion>(entidadDb);

            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, entidadDb);

            await context.SaveChangesAsync();

            return NoContent();

        }

        protected async Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new()
        {
            var existe = await context.Set<TEntidad>().AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new TEntidad() { Id = id });

            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}