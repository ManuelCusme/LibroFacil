namespace LibroFacil.Application.DTOs;

public record LibroDto(int Id, string ISBN, string Titulo, string Autor, int AnioPublicacion, int Stock);

public record CrearLibroDto(string ISBN, string Titulo, string Autor, int AnioPublicacion, int Stock);

public record ActualizarLibroDto(string ISBN, string Titulo, string Autor, int AnioPublicacion, int Stock);