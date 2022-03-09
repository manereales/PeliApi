using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliApi.Entidades;
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
            var entidades = mapper.Map<TEntidad>(creacion);

            if (entidades == null)
            {
                return BadRequest();
            }

            mapper.Map(creacion, entidades);

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