using LibroFacil.Domain.Exceptions;

namespace LibroFacil.Domain.Entities;

public class Libro
{
    public int Id { get; private set; }
    public string ISBN { get; private set; } = string.Empty;
    public string Titulo { get; private set; } = string.Empty;
    public string Autor { get; private set; } = string.Empty;
    public int AnioPublicacion { get; private set; }
    public int Stock { get; private set; }

    // Constructor privado para Entity Framework Core
    private Libro() { }

    public Libro(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        ValidarYAsignar(isbn, titulo, autor, anioPublicacion, stock);
    }

    public void Actualizar(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        ValidarYAsignar(isbn, titulo, autor, anioPublicacion, stock);
    }

public void Vender(int cantidad)
    {
        if (cantidad <= 0)
            throw new DomainException("La cantidad a vender debe ser mayor a cero.");

        if (cantidad > Stock)
            throw new DomainException($"Stock insuficiente. No se puede vender {cantidad} unidades porque solo quedan {Stock} en stock.");

        Stock -= cantidad;
    }

    private void ValidarYAsignar(string isbn, string titulo, string autor, int anioPublicacion, int stock)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new DomainException("El ISBN es obligatorio.");

        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("El título es obligatorio.");

        if (string.IsNullOrWhiteSpace(autor))
            throw new DomainException("El autor es obligatorio.");

        if (anioPublicacion <= 0 || anioPublicacion > DateTime.Now.Year)
            throw new DomainException($"El año de publicación debe ser mayor a 0 y no mayor al año actual ({DateTime.Now.Year}).");

        if (stock < 0)
            throw new DomainException("El stock no puede ser negativo.");

        ISBN = isbn.Trim();
        Titulo = titulo.Trim();
        Autor = autor.Trim();
        AnioPublicacion = anioPublicacion;
        Stock = stock;
    }

    
}