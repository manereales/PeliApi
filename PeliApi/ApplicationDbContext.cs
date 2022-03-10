using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using PeliApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new { x.ActoresId, x.PeliculasId });

            modelBuilder.Entity<PeliculasGeneros>()
               .HasKey(x => new { x.GeneroId, x.PeliculaId });

            modelBuilder.Entity<PeliculasSalasDeCine>()
                .HasKey(x => new { x.PeliculaId, x.SalaDeCineId}); 

            SeedData(modelBuilder);



            base.OnModelCreating(modelBuilder);
        }



        private void SeedData(ModelBuilder modelBuilder)
        {

            //var rolAdminId = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
            //var usuarioAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";

            //var rolAdmin = new IdentityRole()
            //{
            //    Id = rolAdminId,
            //    Name = "Admin",
            //    NormalizedName = "Admin"
            //};

            //var passwordHasher = new PasswordHasher<IdentityUser>();

            //var username = "felipe@hotmail.com";

            //var usuarioAdmin = new IdentityUser()
            //{
            //    Id = usuarioAdminId,
            //    UserName = username,
            //    NormalizedUserName = username,
            //    Email = username,
            //    NormalizedEmail = username,
            //    PasswordHash = passwordHasher.HashPassword(null, "Aa123456!")
            //};

            //modelBuilder.Entity<IdentityUser>()
            //    .HasData(usuarioAdmin);

            //modelBuilder.Entity<IdentityRole>()
            //    .HasData(rolAdmin);

            //modelBuilder.Entity<IdentityUserClaim<string>>()
            //    .HasData(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioAdminId,
            //        ClaimValue = "Admin"
            //    });

            // var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            //modelBuilder.Entity<SalaDeCine>()
            //   .HasData(new List<SalaDeCine>
            //   {
            //        //new SalaDeCine{Id = 1, Nombre = "Agora", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839233))},
            //        new SalaDeCine{Id = 4, Nombre = "Sambil", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9118804, 18.4826214))},
            //        new SalaDeCine{Id = 5, Nombre = "Megacentro", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.856427, 18.506934))},
            //        new SalaDeCine{Id = 6, Nombre = "Village East Cinema", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-73.986227, 40.730898))}
            //   });

            var aventura = new Genero() { Id = 4, Name = "Aventura" };
            var animation = new Genero() { Id = 5, Name = "Animación" };
            var suspenso = new Genero() { Id = 6, Name = "Suspenso" };
            var romance = new Genero() { Id = 7, Name = "Romance" };

            modelBuilder.Entity<Genero>()
                .HasData(new List<Genero>
                {
                        aventura, animation, suspenso, romance
                });

            var jimCarrey = new Actor() { Id = 1005, Nombre = "Jim Carrey", FechaNacimiento = new DateTime(1962, 01, 17) };
            var robertDowney = new Actor() { Id = 1006, Nombre = "Robert Downey Jr.", FechaNacimiento = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor() { Id = 1007, Nombre = "Chris Evans", FechaNacimiento = new DateTime(1981, 06, 13) };

            modelBuilder.Entity<Actor>()
                .HasData(new List<Actor>
                {
                        jimCarrey, robertDowney, chrisEvans
                });

            var endgame = new Peliculas()
            {
                Id = 3,
                Titulo = "Avengers: Endgame",
                EnCines = true,
                FechaEstreno = new DateTime(2019, 04, 26)
            };

            var iw = new Peliculas()
            {
                Id = 4,
                Titulo = "Avengers: Infinity Wars",
                EnCines = false,
                FechaEstreno = new DateTime(2019, 04, 26)
            };

            var sonic = new Peliculas()
            {
                Id = 5,
                Titulo = "Sonic the Hedgehog",
                EnCines = false,
                FechaEstreno = new DateTime(2020, 02, 28)
            };
            var emma = new Peliculas()
            {
                Id = 6,
                Titulo = "Emma",
                EnCines = false,
                FechaEstreno = new DateTime(2020, 02, 21)
            };
            var wonderwoman = new Peliculas()
            {
                Id = 7,
                Titulo = "Wonder Woman 1984",
                EnCines = false,
                FechaEstreno = new DateTime(2022, 08, 14)
            };

            modelBuilder.Entity<Peliculas>()
                .HasData(new List<Peliculas>
                {
                        endgame, iw, sonic, emma, wonderwoman
                });

            modelBuilder.Entity<PeliculasGeneros>().HasData(
                new List<PeliculasGeneros>()
                {
                        new PeliculasGeneros(){PeliculaId = endgame.Id, GeneroId = suspenso.Id},
                        new PeliculasGeneros(){PeliculaId = endgame.Id, GeneroId = aventura.Id},
                        new PeliculasGeneros(){PeliculaId = iw.Id, GeneroId = suspenso.Id},
                        new PeliculasGeneros(){PeliculaId = iw.Id, GeneroId = aventura.Id},
                        new PeliculasGeneros(){PeliculaId = sonic.Id, GeneroId = aventura.Id},
                        new PeliculasGeneros(){PeliculaId = emma.Id, GeneroId = suspenso.Id},
                        new PeliculasGeneros(){PeliculaId = emma.Id, GeneroId = romance.Id},
                        new PeliculasGeneros(){PeliculaId = wonderwoman.Id, GeneroId = suspenso.Id},
                        new PeliculasGeneros(){PeliculaId = wonderwoman.Id, GeneroId = aventura.Id},
                });

            modelBuilder.Entity<PeliculasActores>().HasData(
                new List<PeliculasActores>()
                {
                    new PeliculasActores(){PeliculasId = endgame.Id, ActoresId = robertDowney.Id, Personaje = "Tony Stark", Orden = 1},
                    new PeliculasActores(){PeliculasId = endgame.Id, ActoresId = chrisEvans.Id, Personaje = "Steve Rogers", Orden = 2},
                    new PeliculasActores(){PeliculasId = iw.Id, ActoresId = robertDowney.Id, Personaje = "Tony Stark", Orden = 1},
                    new PeliculasActores(){PeliculasId = iw.Id, ActoresId = chrisEvans.Id, Personaje = "Steve Rogers", Orden = 2},
                    new PeliculasActores(){PeliculasId = sonic.Id, ActoresId = jimCarrey.Id, Personaje = "Dr. Ivo Robotnik", Orden = 1}
                });
        }



        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Peliculas> Peliculas { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<SalaDeCine> SalaDeCines { get; set; }
        public DbSet<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; } 



    }
}
