using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Sabor_Do_Brasil
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Nome).IsUnique();
                entity.Property(e => e.Nickname).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Nickname).IsUnique();
            });
        }
    }
}