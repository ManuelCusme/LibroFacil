using LibroFacil.Domain.Entities;

namespace LibroFacil.Application.Interfaces;

public interface ILibroRepository
{
    Task<IEnumerable<Libro>> ObtenerTodosAsync();
    Task<Libro?> ObtenerPorIdAsync(int id);
    Task<bool> ExisteIsbnAsync(string isbn, int? idExcluir = null);
    Task AgregarAsync(Libro libro);
    Task ActualizarAsync(Libro libro);
    Task EliminarAsync(Libro libro);
}