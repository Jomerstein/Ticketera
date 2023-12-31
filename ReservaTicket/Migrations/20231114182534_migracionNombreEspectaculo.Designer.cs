﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservaTicket.Context;

#nullable disable

namespace ReservaTicket.Migrations
{
    [DbContext(typeof(TicketeraDataBaseContext))]
    [Migration("20231114182534_migracionNombreEspectaculo")]
    partial class migracionNombreEspectaculo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReservaTicket.Models.Entrada", b =>
                {
                    b.Property<int>("idEntrada")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEntrada"));

                    b.Property<string>("codigoEntrada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("estaUsada")
                        .HasColumnType("bit");

                    b.Property<bool>("estaVendida")
                        .HasColumnType("bit");

                    b.Property<int>("idEspectaculo")
                        .HasColumnType("int");

                    b.HasKey("idEntrada");

                    b.HasIndex("idEspectaculo");

                    b.ToTable("entrada");
                });

            modelBuilder.Entity("ReservaTicket.Models.Espectaculo", b =>
                {
                    b.Property<int>("idEspectaculo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idEspectaculo"));

                    b.Property<int>("cantEntradas")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaEspectaculo")
                        .HasColumnType("datetime2");

                    b.Property<string>("nombreEspectaculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("usuarioID")
                        .HasColumnType("int");

                    b.HasKey("idEspectaculo");

                    b.HasIndex("usuarioID");

                    b.ToTable("espectaculo");
                });

            modelBuilder.Entity("ReservaTicket.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomUsuario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("contrasenia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("ReservaTicket.Models.Entrada", b =>
                {
                    b.HasOne("ReservaTicket.Models.Espectaculo", "espectaculo")
                        .WithMany()
                        .HasForeignKey("idEspectaculo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("espectaculo");
                });

            modelBuilder.Entity("ReservaTicket.Models.Espectaculo", b =>
                {
                    b.HasOne("ReservaTicket.Models.Usuario", "creador")
                        .WithMany()
                        .HasForeignKey("usuarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("creador");
                });
#pragma warning restore 612, 618
        }
    }
}
