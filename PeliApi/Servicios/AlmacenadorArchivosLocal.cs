using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Servicios
{
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env; //con el "env" obtendremos la ruta donde se encuentra nuestro wwwroot para colocar archivos
            this.httpContextAccessor = httpContextAccessor; //con el httpcontextaccesor vamos a determinar el dominio donde tenemos publicado nuestro web api y asi
           //vamos a construir la url que se va guardar en la base de datos de actores
        }

        public Task BorrarArchivo(string ruta, string contenedor)
        {
            if (ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta); // extraemos de la ruta
                string directorioArchivo = Path.Combine(env.WebRootPath, nombreArchivo, contenedor);

                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
                
            }

            return Task.FromResult(0);
        }

    
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string contentType, string ruta)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}"; //
            string folder = Path.Combine(env.WebRootPath, contenedor); //aqui combinamos la direccion del wwwroot con la carpeta contenedor para segementar las imagenes en diferentes directorios

            if (!Directory.Exists(folder)) // por si no existe nosotros creamos el directorio
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);

            await File.WriteAllBytesAsync(ruta, contenido); // estamos escribiendo en el disco duro el contenido del archivo

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, contenedor, nombreArchivo).Replace("\\", "/");
            return urlParaBD;
        }
    }
}
