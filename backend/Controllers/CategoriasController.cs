using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _db;

    public CategoriasController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> Listar()
    {
        var categorias = await _db.Categorias
            .OrderBy(c => c.Nome)
            .Select(c => new CategoriaDto(c.Id, c.Nome, c.Ativo))
            .ToListAsync();

        return Ok(categorias);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Criar(CategoriaDto dto)
    {
        var categoria = new Categoria { Nome = dto.Nome, Ativo = dto.Ativo };
        _db.Categorias.Add(categoria);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Listar), new CategoriaDto(categoria.Id, categoria.Nome, categoria.Ativo));
    }
}
