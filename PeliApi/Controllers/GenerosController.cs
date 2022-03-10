using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliApi.DTO_s;
using PeliApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: CustomBaseController
    {
       

        public GenerosController(ApplicationDbContext context, IMapper mapper): base(context, mapper)
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            //var  entidades = await context.Generos.ToListAsync();
            //var dtos = mapper.Map<List<GeneroDTO>>(entidades);
            //return dtos;

            return await Get<Genero, GeneroDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            //var entidades = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            //if (entidades == null)
            //{
            //    return NotFound();
            //}
            //var dtos = mapper.Map<GeneroDTO>(entidades);
            //return dtos;

            return await Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreacionGeneroDTO creacionGeneroDTO) 
        {
            //var entidad = mapper.Map<Genero>(creacionGeneroDTO);
            //context.Add(entidad);
            //await context.SaveChangesAsync();
            //var generoDTO = mapper.Map<GeneroDTO>(entidad);

            //return new CreatedAtRouteResult("obtenerGenero", new { id = generoDTO.Id }, generoDTO);

            return  await Post<CreacionGeneroDTO, Genero, GeneroDTO>(creacionGeneroDTO, "obtenerGenero");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]CreacionGeneroDTO creacionGenero) 
        {
            //var entidad = mapper.Map<Genero>(creacionGenero);
            //entidad.Id = id;
            //context.Entry(entidad).State = EntityState.Modified;
            //await context.SaveChangesAsync();
            //return NoContent();

            return await Put<CreacionGeneroDTO, Genero>(creacionGenero, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            //var existe = await context.Generos.AnyAsync(x => x.Id == id);
            //if (!existe)
            //{
            //    return NotFound();
            //}

            //context.Remove(new Genero() { Id = id });
            //await context.SaveChangesAsync();

            //return NoContent();


            return await Delete<Genero>(id);

        }
    }
}
