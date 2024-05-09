﻿// <auto-generated />
using System;
using ApiBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiBiblioteca.Migrations
{
    [DbContext(typeof(LibrosDbContext))]
    [Migration("20240509051622_IncorporacionDatosAdicionales")]
    partial class IncorporacionDatosAdicionales
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiBiblioteca.Models.LibrosME", b =>
                {
                    b.Property<Guid>("IDLibro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApellidoAutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Disponible")
                        .HasColumnType("bit");

                    b.Property<string>("Editorial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lugar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreAutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tematica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloLibro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDLibro");

                    b.ToTable("LibrosME");
                });
#pragma warning restore 612, 618
        }
    }
}
