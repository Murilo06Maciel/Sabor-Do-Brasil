using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sabor_Do_Brasil.Models;

namespace Sabor_Do_Brasil
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ReceitaCategoria> ReceitaCategorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuração da tabela de ligação N:N entre Receita e Categoria
            builder.Entity<ReceitaCategoria>()
                .HasKey(rc => new { rc.ReceitaId, rc.CategoriaId });

            builder.Entity<ReceitaCategoria>()
                .HasOne(rc => rc.Receita)
                .WithMany(r => r.ReceitaCategorias)
                .HasForeignKey(rc => rc.ReceitaId);

            builder.Entity<ReceitaCategoria>()
                .HasOne(rc => rc.Categoria)
                .WithMany(c => c.ReceitaCategorias)
                .HasForeignKey(rc => rc.CategoriaId);
        }
    }
}