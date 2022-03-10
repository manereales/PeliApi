﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeliApi;

namespace PeliApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220310165847_TablasSalasdeCine")]
    partial class TablasSalasdeCine
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PeliApi.Entidades.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Actores");

                    b.HasData(
                        new
                        {
                            Id = 1005,
                            FechaNacimiento = new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Jim Carrey"
                        },
                        new
                        {
                            Id = 1006,
                            FechaNacimiento = new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Robert Downey Jr."
                        },
                        new
                        {
                            Id = 1007,
                            FechaNacimiento = new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Chris Evans"
                        });
                });

            modelBuilder.Entity("PeliApi.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Name = "Aventura"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Animación"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Suspenso"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Romance"
                        });
                });

            modelBuilder.Entity("PeliApi.Entidades.Peliculas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EnCines")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("datetime2");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Peliculas");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            EnCines = true,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Endgame"
                        },
                        new
                        {
                            Id = 4,
                            EnCines = false,
                            FechaEstreno = new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Avengers: Infinity Wars"
                        },
                        new
                        {
                            Id = 5,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Sonic the Hedgehog"
                        },
                        new
                        {
                            Id = 6,
                            EnCines = false,
                            FechaEstreno = new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Emma"
                        },
                        new
                        {
                            Id = 7,
                            EnCines = false,
                            FechaEstreno = new DateTime(2022, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titulo = "Wonder Woman 1984"
                        });
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasActores", b =>
                {
                    b.Property<int>("ActoresId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculasId")
                        .HasColumnType("int");

                    b.Property<int?>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<string>("Personaje")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActoresId", "PeliculasId");

                    b.HasIndex("ActorId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("PeliculasActores");

                    b.HasData(
                        new
                        {
                            ActoresId = 1006,
                            PeliculasId = 3,
                            Orden = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActoresId = 1007,
                            PeliculasId = 3,
                            Orden = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActoresId = 1006,
                            PeliculasId = 4,
                            Orden = 1,
                            Personaje = "Tony Stark"
                        },
                        new
                        {
                            ActoresId = 1007,
                            PeliculasId = 4,
                            Orden = 2,
                            Personaje = "Steve Rogers"
                        },
                        new
                        {
                            ActoresId = 1005,
                            PeliculasId = 5,
                            Orden = 1,
                            Personaje = "Dr. Ivo Robotnik"
                        });
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasGeneros", b =>
                {
                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("GeneroId", "PeliculaId");

                    b.HasIndex("PeliculasId");

                    b.ToTable("PeliculasGeneros");

                    b.HasData(
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 3
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 4
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 4
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 5
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 6
                        },
                        new
                        {
                            GeneroId = 7,
                            PeliculaId = 6
                        },
                        new
                        {
                            GeneroId = 6,
                            PeliculaId = 7
                        },
                        new
                        {
                            GeneroId = 4,
                            PeliculaId = 7
                        });
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasSalasDeCine", b =>
                {
                    b.Property<int>("PeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("SalaDeCineId")
                        .HasColumnType("int");

                    b.Property<int?>("PeliculasId")
                        .HasColumnType("int");

                    b.HasKey("PeliculaId", "SalaDeCineId");

                    b.HasIndex("PeliculasId");

                    b.HasIndex("SalaDeCineId");

                    b.ToTable("PeliculasSalasDeCines");
                });

            modelBuilder.Entity("PeliApi.Entidades.SalaDeCine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("SalaDeCines");
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasActores", b =>
                {
                    b.HasOne("PeliApi.Entidades.Actor", "Actor")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("ActorId");

                    b.HasOne("PeliApi.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasActores")
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasGeneros", b =>
                {
                    b.HasOne("PeliApi.Entidades.Genero", "Genero")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliApi.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasGeneros")
                        .HasForeignKey("PeliculasId");

                    b.Navigation("Genero");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasSalasDeCine", b =>
                {
                    b.HasOne("PeliApi.Entidades.Peliculas", "Peliculas")
                        .WithMany("PeliculasSalasDeCines")
                        .HasForeignKey("PeliculasId");

                    b.HasOne("PeliApi.Entidades.SalaDeCine", "SalaDeCine")
                        .WithMany("PeliculasSalasDeCines")
                        .HasForeignKey("SalaDeCineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Peliculas");

                    b.Navigation("SalaDeCine");
                });

            modelBuilder.Entity("PeliApi.Entidades.Actor", b =>
                {
                    b.Navigation("PeliculasActores");
                });

            modelBuilder.Entity("PeliApi.Entidades.Genero", b =>
                {
                    b.Navigation("PeliculasGeneros");
                });

            modelBuilder.Entity("PeliApi.Entidades.Peliculas", b =>
                {
                    b.Navigation("PeliculasActores");

                    b.Navigation("PeliculasGeneros");

                    b.Navigation("PeliculasSalasDeCines");
                });

            modelBuilder.Entity("PeliApi.Entidades.SalaDeCine", b =>
                {
                    b.Navigation("PeliculasSalasDeCines");
                });
#pragma warning restore 612, 618
        }
    }
}
