using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TamanhosController : ControllerBase
{
    private readonly AppDbContext _db;

    public TamanhosController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TamanhoDto>>> Listar()
    {
        var tamanhos = await _db.Tamanhos
            .OrderBy(t => t.Fator)
            .Select(t => new TamanhoDto(t.Id, t.Nome, t.Fatias, t.Fator))
            .ToListAsync();

        return Ok(tamanhos);
    }
}
