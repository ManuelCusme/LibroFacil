using LibroFacil.Application.DTOs;
using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;
using LibroFacil.Domain.Exceptions;

namespace LibroFacil.Application.Services;

public class LibroService
{
    private readonly ILibroRepository _repository;

    public LibroService(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LibroDto>> ObtenerTodosAsync()
    {
        var libros = await _repository.ObtenerTodosAsync();
        return libros.Select(l => new LibroDto(l.Id, l.ISBN, l.Titulo, l.Autor, l.AnioPublicacion, l.Stock));
    }

    public async Task<LibroDto?> ObtenerPorIdAsync(int id)
    {
        var libro = await _repository.ObtenerPorIdAsync(id);
        return libro == null ? null : new LibroDto(libro.Id, libro.ISBN, libro.Titulo, libro.Autor, libro.AnioPublicacion, libro.Stock);
    }

    public async Task<LibroDto> CrearAsync(CrearLibroDto dto)
    {
        if (await _repository.ExisteIsbnAsync(dto.ISBN))
            throw new DomainException("El ISBN ya se encuentra registrado.");

        var libro = new Libro(dto.ISBN, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
        await _repository.AgregarAsync(libro);

        return new LibroDto(libro.Id, libro.ISBN, libro.Titulo, libro.Autor, libro.AnioPublicacion, libro.Stock);
    }

    public async Task<bool> ActualizarAsync(int id, ActualizarLibroDto dto)
    {
        var libro = await _repository.ObtenerPorIdAsync(id);
        if (libro == null) return false;

        if (await _repository.ExisteIsbnAsync(dto.ISBN, id))
            throw new DomainException("El ISBN ya está registrado en otro libro.");

        libro.Actualizar(dto.ISBN, dto.Titulo, dto.Autor, dto.AnioPublicacion, dto.Stock);
        await _repository.ActualizarAsync(libro);
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var libro = await _repository.ObtenerPorIdAsync(id);
        if (libro == null) return false;

        await _repository.EliminarAsync(libro);
        return true;
    }

    public async Task<bool> VenderLibroAsync(int id, int cantidad)
{
    var libro = await _repository.ObtenerPorIdAsync(id);
    if (libro == null) return false;

    // Ejecuta la regla de negocio protegida desde el dominio
    libro.Vender(cantidad);

    // Persiste el cambio en SQL Server a través del repositorio
    await _repository.ActualizarAsync(libro);
    return true;
}
}