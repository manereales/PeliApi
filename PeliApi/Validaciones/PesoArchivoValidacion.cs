using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Validaciones
{
    public class PesoArchivoValidacion: ValidationAttribute
    {
        private readonly int pesoMAximoEnMegabytes;

        public PesoArchivoValidacion(int pesoMAximoEnMegabytes)
        {
            this.pesoMAximoEnMegabytes = pesoMAximoEnMegabytes;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;


            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > pesoMAximoEnMegabytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMAximoEnMegabytes}mb");
            }

            return ValidationResult.Success;
        }
    }
}
