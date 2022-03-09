using AutoMapper;
using PeliApi.DTO_s;
using PeliApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<CreacionGeneroDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore());        
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();
            
            CreateMap<Peliculas, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Peliculas>().ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Peliculas, PeliculasDetalleDTO>().ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros)).
                ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores)).ReverseMap();

            CreateMap<PeliculaPatchDTO, Peliculas>().ReverseMap();


            


        }


        private List<GeneroDTO> MapPeliculasGeneros(Peliculas peliculas, PeliculasDetalleDTO peliculasDetalle)
        {
            var resultado = new List<GeneroDTO>();

            if (peliculas.PeliculasGeneros == null)
            {
                return resultado;
            }

            foreach (var generoPelicula in peliculas.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO() {  Id = generoPelicula.GeneroId, Name = generoPelicula.Genero.Name });
            }

            return resultado;
        }

        private List<ActorPeliculaDetalleDTO> MapPeliculasActores(Peliculas peliculas, PeliculasDetalleDTO peliculasDetalle)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();

            if (peliculas.PeliculasGeneros == null)
            {
                return resultado;
            }

            foreach (var actorPelicula in peliculas.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO() { ActorId = actorPelicula.ActoresId, NombrePersona = actorPelicula.Actor.Nombre, Personaje = actorPelicula.Personaje});
            }

            return resultado;
        }


        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Peliculas peliculas) 
        {

            var resultado = new List<PeliculasGeneros>();

            if (peliculaCreacionDTO.GenerosIds == null)
            {
                return resultado;
            }
            foreach (var id in peliculaCreacionDTO.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }

            return resultado;
        }


        private List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Peliculas peliculas) 
        {

            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDTO.Actores == null)
            {
                return resultado;
            }
            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActoresId = actor.ActorId, Personaje = actor.Personaje });
            }

            return resultado;
        }
    }
}
