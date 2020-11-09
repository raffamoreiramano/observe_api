﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Observe.Data;

namespace Observe.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201109013047_ValueComparers")]
    partial class ValueComparers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Observe.Models.Medico", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<int>("UID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UID")
                        .IsUnique();

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("Observe.Models.Paciente", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alergias")
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<string>("Doencas")
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remedios")
                        .HasColumnType("NVARCHAR(255)");

                    b.Property<int>("UID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UID")
                        .IsUnique();

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("Observe.Models.Receita", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MID")
                        .HasColumnType("int");

                    b.Property<int>("PID")
                        .HasColumnType("int");

                    b.Property<string>("Remedios")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.HasKey("ID");

                    b.HasIndex("MID");

                    b.HasIndex("PID");

                    b.ToTable("Receita");
                });

            modelBuilder.Entity("Observe.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CID")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnName("Sobrenome")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("CID")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Observe.Models.Medico", b =>
                {
                    b.HasOne("Observe.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Observe.Models.Paciente", b =>
                {
                    b.HasOne("Observe.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Observe.Models.Receita", b =>
                {
                    b.HasOne("Observe.Models.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Observe.Models.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
