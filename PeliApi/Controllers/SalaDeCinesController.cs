using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliApi.DTO_s;
using PeliApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Controllers
{
    [ApiController]
    [Route("api/salasDeCines")]
    public class SalaDeCinesController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SalaDeCinesController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<List<SalaDeCineDTO>> Get()
        {
            return await Get<SalaDeCine, SalaDeCineDTO>();
        }

        [HttpGet("{id}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDTO>> Get(int id)
        {
            return await Get<SalaDeCine, SalaDeCineDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] SalaDeCineCreacionDTO salaDeCineCreacion)
        {
            return await Post<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineCreacion, "obtenerSalaDeCine");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromForm] SalaDeCineCreacionDTO salaDeCineCreacion)
        {
            return await Put<SalaDeCineCreacionDTO, SalaDeCine>(salaDeCineCreacion, id);

        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<SalaDeCineCreacionDTO> patchDocument)
        {
            return await Patch<SalaDeCineCreacionDTO, SalaDeCine>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<SalaDeCine>(id);
        }
    }
}
