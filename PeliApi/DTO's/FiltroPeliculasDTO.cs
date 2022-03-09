using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.DTO_s
{
    public class FiltroPeliculasDTO
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistroPorPagina { get; set; } = 10;
        public PaginacionDTO paginacion
        {
            get { return new PaginacionDTO() { Pagina = Pagina, CantidadRegistroPorPagina = CantidadRegistroPorPagina }; }
        }

        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }
        public string  campoOrdenar { get; set; }
        public bool OrdenAscendente { get; set; } = true;


    }
}
