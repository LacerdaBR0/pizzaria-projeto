using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly AppDbContext _db;

    public RelatorioController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("vendas")]
    public async Task<ActionResult<RelatorioDto>> Vendas()
    {
        var totalFaturado = await _db.Pedidos.SumAsync(p => (decimal?)p.Total) ?? 0m;
        var totalPedidos = await _db.Pedidos.CountAsync();
        var ticketMedio = totalPedidos > 0 ? Math.Round(totalFaturado / totalPedidos, 2) : 0m;

        var porStatus = await _db.Pedidos
            .GroupBy(p => p.Status)
            .Select(g => new ContagemStatusDto(g.Key, g.Count()))
            .ToListAsync();

        var itens = await _db.PedidoItens
            .Select(i => new { i.Descricao, i.Quantidade })
            .ToListAsync();

        var maisVendidas = itens
            .GroupBy(i => i.Descricao)
            .Select(g => new MaisVendidaDto(g.Key, g.Sum(x => x.Quantidade)))
            .OrderByDescending(m => m.Quantidade)
            .Take(5)
            .ToList();

        return Ok(new RelatorioDto(totalFaturado, totalPedidos, ticketMedio, porStatus, maisVendidas));
    }
}
