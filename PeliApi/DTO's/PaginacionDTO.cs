using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.DTO_s
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;

        private int cantidadRegistroPorPagina = 10;
        private readonly int cantidadMaximaRegistroPorPagina  = 10;


        public int CantidadRegistroPorPagina 
        {
            get => cantidadRegistroPorPagina;

            //set => this.cantidadRegistroPorPagina = (value > cantidadMaximaRegistroPorPagina) ? cantidadMaximaRegistroPorPagina : value;
            set
            {
                if (value >= cantidadMaximaRegistroPorPagina)
                {
                    cantidadRegistroPorPagina = cantidadMaximaRegistroPorPagina;
                }
                else
                {
                    cantidadRegistroPorPagina = value;
                }
                
            }

        }
    }
}
