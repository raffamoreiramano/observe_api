using Observe.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Observe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Receita> Receitas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // gerando tabelas
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Medico>().ToTable("Medico");
            modelBuilder.Entity<Paciente>().ToTable("Paciente");
            modelBuilder.Entity<Receita>().ToTable("Receita");

            // gerando indices
            modelBuilder.Entity<Usuario>().HasIndex(u => u.CID).IsUnique();
            modelBuilder.Entity<Medico>().HasIndex(m => m.UID).IsUnique();
            modelBuilder.Entity<Paciente>().HasIndex(p => p.UID).IsUnique();

            // convertendo listas em json
            modelBuilder.Entity<Paciente>().Property(p => p.Doencas)
                .HasConversion(
                    lista => JsonConvert.SerializeObject(lista),
                    lista => JsonConvert.DeserializeObject<List<string>>(lista));
            modelBuilder.Entity<Paciente>().Property(p => p.Alergias)
                .HasConversion(
                    lista => JsonConvert.SerializeObject(lista),
                    lista => JsonConvert.DeserializeObject<List<string>>(lista));
            modelBuilder.Entity<Paciente>().Property(p => p.Remedios)
                .HasConversion(
                    lista => JsonConvert.SerializeObject(lista),
                    lista => JsonConvert.DeserializeObject<List<string>>(lista));
            modelBuilder.Entity<Receita>().Property(p => p.Remedios)
                .HasConversion(
                    lista => JsonConvert.SerializeObject(lista),
                    lista => JsonConvert.DeserializeObject<List<string>>(lista));

            // mudando tipo datetime para datetime2
            modelBuilder.Entity<Paciente>().Property(p => p.Nascimento).HasColumnType("datetime2");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}