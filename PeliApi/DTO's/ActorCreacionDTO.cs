using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PeliApi.Validaciones;

namespace PeliApi.DTO_s
{
    public class ActorCreacionDTO: ActorPatchDTO
    {
        //[Required]
        //[StringLength(40)]
        //public string Nombre { get; set; }
        //public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(pesoMAximoEnMegabytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
