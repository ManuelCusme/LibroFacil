using LibroFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Persistence;

public class LibroFacilDbContext : DbContext
{
    public LibroFacilDbContext(DbContextOptions<LibroFacilDbContext> options) : base(options) { }

    public DbSet<Libro> Libros => Set<Libro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ISBN).IsUnique(); // Garantiza la regla de ISBN único en la base de datos
            entity.Property(e => e.ISBN).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Autor).IsRequired().HasMaxLength(100);
        });
    }
}