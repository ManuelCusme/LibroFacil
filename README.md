# LibroFácil - Web API con Clean Architecture

Web API desarrollada en C# y .NET 8.0 para la gestión de catálogo de libros, implementando Clean Architecture, principios SOLID, DDD básico y persistencia de datos mediante Entity Framework Core con SQL Server.

---

## Estructura del Proyecto

```text
LibroFacil/
├── LibroFacil.sln
└── src/
    ├── LibroFacil.Domain/
    │   ├── Entities/
    │   │   └── Libro.cs
    │   └── Exceptions/
    │       └── DomainException.cs
    ├── LibroFacil.Application/
    │   ├── DTOs/
    │   │   └── LibroDtos.cs
    │   ├── Interfaces/
    │   │   └── ILibroRepository.cs
    │   └── Services/
    │       └── LibroService.cs
    ├── LibroFacil.Infrastructure/
    │   ├── Migrations/
    │   ├── Persistence/
    │   │   └── LibroFacilDbContext.cs
    │   └── Repositories/
    │       └── LibroRepositoryEf.cs
    └── LibroFacil.Api/
        ├── Controllers/
        │   └── LibrosController.cs
        ├── Program.cs
        └── appsettings.json
```

---

## Arquitectura y Principios Aplicados

- **Clean Architecture**: separación estricta de responsabilidades entre Domain, Application, Infrastructure y Api.
- **Single Responsibility Principle (SRP)**: cada clase posee una única responsabilidad.
- **Dependency Inversion Principle (DIP)**: Application depende de la abstracción `ILibroRepository`.
- **DDD básico**: entidad `Libro` con setters privados y reglas de negocio encapsuladas.

---

## Reglas de Negocio Implementadas

- ISBN obligatorio y único.
- Título obligatorio.
- Autor obligatorio.
- Año de publicación mayor a 0 y menor o igual al año actual.
- Stock no negativo.

---

## Endpoints de la API

| Método | Endpoint            | Descripción             |
|--------|----------------------|--------------------------|
| GET    | `/api/libros`         | Listar todos los libros  |
| GET    | `/api/libros/{id}`    | Consultar libro por Id   |
| POST   | `/api/libros`         | Agregar un libro         |
| PUT    | `/api/libros/{id}`    | Actualizar un libro      |
| DELETE | `/api/libros/{id}`    | Eliminar un libro        |

---

## Ejecución

**Restaurar e impactar base de datos en SQL Server:**

```bash
dotnet ef database update --project src/LibroFacil.Infrastructure/LibroFacil.Infrastructure.csproj --startup-project src/LibroFacil.Api/LibroFacil.Api.csproj
```

**Iniciar la API:**

```bash
dotnet run --project src/LibroFacil.Api/LibroFacil.Api.csproj
```
