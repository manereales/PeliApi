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
    [Migration("20220218230016_PeliculasActores_PeliculasGeneros")]
    partial class PeliculasActores_PeliculasGeneros
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
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasActores", b =>
                {
                    b.HasOne("PeliApi.Entidades.Actor", "Actor")
                        .WithMany()
                        .HasForeignKey("ActorId");

                    b.HasOne("PeliApi.Entidades.Peliculas", "Peliculas")
                        .WithMany()
                        .HasForeignKey("PeliculasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("PeliApi.Entidades.PeliculasGeneros", b =>
                {
                    b.HasOne("PeliApi.Entidades.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeliApi.Entidades.Peliculas", "Peliculas")
                        .WithMany()
                        .HasForeignKey("PeliculasId");

                    b.Navigation("Genero");

                    b.Navigation("Peliculas");
                });
#pragma warning restore 612, 618
        }
    }
}