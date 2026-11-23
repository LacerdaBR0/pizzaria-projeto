using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _db;

    private static readonly string[] StatusValidos =
    {
        "Recebido", "Em preparo", "Saiu pra entrega", "Entregue"
    };

    public PedidosController(AppDbContext db)
    {
        _db = db;
    }

    private static List<PedidoItemDto> MontarItens(Pedido p)
    {
        return p.Itens
            .Select(i => new PedidoItemDto(i.Descricao, i.Quantidade, i.PrecoUnitario, i.PrecoUnitario * i.Quantidade))
            .ToList();
    }

    private static PedidoDto MontarDto(Pedido p)
    {
        return new PedidoDto(p.Id, p.CriadoEm, p.Status, p.Endereco, p.FormaPagamento, p.Total, MontarItens(p));
    }

    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Finalizar(FinalizarPedidoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Endereco) || string.IsNullOrWhiteSpace(dto.FormaPagamento))
            return BadRequest("Informe o endereço e a forma de pagamento.");

        var usuario = await _db.Usuarios.FindAsync(dto.UsuarioId);
        if (usuario is null) return BadRequest("Usuário inválido.");

        var carrinho = await _db.Carrinhos
            .Include(c => c.Itens)
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();

        if (carrinho is null || carrinho.Itens.Count == 0)
            return BadRequest("O carrinho está vazio.");

        var pedido = new Pedido
        {
            UsuarioId = usuario.Id,
            Endereco = dto.Endereco.Trim(),
            FormaPagamento = dto.FormaPagamento.Trim(),
            Status = "Recebido",
            CriadoEm = DateTime.Now
        };

        foreach (var item in carrinho.Itens)
        {
            pedido.Itens.Add(new PedidoItem
            {
                Descricao = item.Descricao,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario
            });
        }

        pedido.Total = pedido.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

        _db.Pedidos.Add(pedido);
        _db.CarrinhoItens.RemoveRange(carrinho.Itens);
        await _db.SaveChangesAsync();

        return Ok(MontarDto(pedido));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PedidoDto>>> Listar([FromQuery] int usuarioId)
    {
        var pedidos = await _db.Pedidos
            .Include(p => p.Itens)
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync();

        return Ok(pedidos.Select(MontarDto).ToList());
    }

    [HttpGet("todos")]
    public async Task<ActionResult<IEnumerable<PedidoAdminDto>>> Todos()
    {
        var pedidos = await _db.Pedidos
            .Include(p => p.Itens)
            .Include(p => p.Usuario)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync();

        var dtos = pedidos.Select(p => new PedidoAdminDto(
            p.Id,
            p.CriadoEm,
            p.Usuario!.Nome,
            p.Status,
            p.Endereco,
            p.FormaPagamento,
            p.Total,
            MontarItens(p))).ToList();

        return Ok(dtos);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<PedidoAdminDto>> AtualizarStatus(int id, AtualizarStatusDto dto)
    {
        if (!StatusValidos.Contains(dto.Status))
            return BadRequest("Status inválido.");

        var pedido = await _db.Pedidos
            .Include(p => p.Itens)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido is null) return NotFound();

        pedido.Status = dto.Status;
        await _db.SaveChangesAsync();

        return Ok(new PedidoAdminDto(
            pedido.Id,
            pedido.CriadoEm,
            pedido.Usuario!.Nome,
            pedido.Status,
            pedido.Endereco,
            pedido.FormaPagamento,
            pedido.Total,
            MontarItens(pedido)));
    }
}
