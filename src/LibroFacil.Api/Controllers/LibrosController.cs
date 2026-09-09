using LibroFacil.Application.DTOs;
using LibroFacil.Application.Services;
using LibroFacil.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibroFacil.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly LibroService _service;

    public LibrosController(LibroService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var libros = await _service.ObtenerTodosAsync();
        return Ok(libros);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var libro = await _service.ObtenerPorIdAsync(id);
        if (libro == null) return NotFound(new { mensaje = $"No se encontró el libro con Id {id}" });
        return Ok(libro);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearLibroDto dto)
    {
        try
        {
            var creado = await _service.CrearAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }
        catch (DomainException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ActualizarLibroDto dto)
    {
        try
        {
            var actualizado = await _service.ActualizarAsync(id, dto);
            if (!actualizado) return NotFound(new { mensaje = $"No se encontró el libro con Id {id}" });
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var eliminado = await _service.EliminarAsync(id);
        if (!eliminado) return NotFound(new { mensaje = $"No se encontró el libro con Id {id}" });
        return NoContent();
    }
}