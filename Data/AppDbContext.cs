using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Sabor_Do_Brasil;

namespace Sabor_Do_Brasil
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Defina suas tabelas como DbSets
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo podem ser feitas aqui
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NOME).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Nickname).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Nickname).IsUnique();
                entity.Property(e => e.Senha).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.SENHA).IsUnique();
                entity.HasIndex(e => e.FOTO).IsUnique();
                entity.HasIndex(e => e.createdAt).IsUnique();
                entity.HasIndex(e => e.updatedAt).IsUnique();
            });
        }
    }
}