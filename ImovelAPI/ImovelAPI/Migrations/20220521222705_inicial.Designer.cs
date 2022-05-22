﻿// <auto-generated />
using ImovelAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImovelAPI.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20220521222705_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("ImovelAPI.Model.Imovel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Alugado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Bairro")
                        .HasColumnType("text");

                    b.Property<string>("Cep")
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasColumnType("text");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Referencia")
                        .HasColumnType("text");

                    b.Property<string>("Rua")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Imovel");
                });
#pragma warning restore 612, 618
        }
    }
}