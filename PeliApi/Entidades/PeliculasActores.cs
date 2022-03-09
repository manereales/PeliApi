using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Entidades
{
    public class PeliculasActores
    {
        public int ActoresId { get; set; }
        public int PeliculasId { get; set; }
        public string Personaje { get; set; }
        public int Orden { get; set; }
        public Actor Actor { get; set; }
        public Peliculas Peliculas { get; set; }
    }
}
