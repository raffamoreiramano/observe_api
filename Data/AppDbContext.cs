using Observe.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

            // adicionando comparadores às listas
            modelBuilder.Entity<Paciente>().Property(p => p.Doencas)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                    lista => lista == null ? 0 : JsonConvert.SerializeObject(lista).GetHashCode(),
                    lista => JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(lista))));
            modelBuilder.Entity<Paciente>().Property(p => p.Alergias)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                    lista => lista == null ? 0 : JsonConvert.SerializeObject(lista).GetHashCode(),
                    lista => JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(lista))));
            modelBuilder.Entity<Paciente>().Property(p => p.Remedios)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                    lista => lista == null ? 0 : JsonConvert.SerializeObject(lista).GetHashCode(),
                    lista => JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(lista))));
            modelBuilder.Entity<Receita>().Property(r => r.Remedios)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                    lista => lista == null ? 0 : JsonConvert.SerializeObject(lista).GetHashCode(),
                    lista => JsonConvert.DeserializeObject<List<string>>(JsonConvert.SerializeObject(lista))));

            // mudando tipo datetime para datetime2
            modelBuilder.Entity<Paciente>().Property(p => p.Nascimento).HasColumnType("datetime2");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}