using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhoController : ControllerBase
{
    private readonly AppDbContext _db;

    public CarrinhoController(AppDbContext db)
    {
        _db = db;
    }

    private async Task<Carrinho> ObterOuCriarCarrinho()
    {
        var carrinho = await _db.Carrinhos
            .Include(c => c.Itens)
            .OrderBy(c => c.Id)
            .FirstOrDefaultAsync();

        if (carrinho is null)
        {
            carrinho = new Carrinho();
            _db.Carrinhos.Add(carrinho);
            await _db.SaveChangesAsync();
        }

        return carrinho;
    }

    private async Task<CarrinhoDto> MontarCarrinhoDto(int carrinhoId)
    {
        var itens = await _db.CarrinhoItens
            .Include(i => i.Tamanho)
            .Where(i => i.CarrinhoId == carrinhoId)
            .OrderBy(i => i.Id)
            .Select(i => new CarrinhoItemDto(
                i.Id,
                i.Descricao,
                i.Tamanho!.Nome,
                i.Quantidade,
                i.PrecoUnitario,
                i.PrecoUnitario * i.Quantidade))
            .ToListAsync();

        var total = itens.Sum(i => i.Subtotal);
        return new CarrinhoDto(carrinhoId, itens, total);
    }

    [HttpGet]
    public async Task<ActionResult<CarrinhoDto>> Obter()
    {
        var carrinho = await ObterOuCriarCarrinho();
        return Ok(await MontarCarrinhoDto(carrinho.Id));
    }

    [HttpPost("itens")]
    public async Task<ActionResult<CarrinhoDto>> AdicionarItem(MontarItemDto dto)
    {
        if (dto.Quantidade < 1) return BadRequest("Quantidade deve ser pelo menos 1.");

        var tamanho = await _db.Tamanhos.FindAsync(dto.TamanhoId);
        if (tamanho is null) return BadRequest("Tamanho inválido.");

        var sabor1 = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == dto.Sabor1Id && p.Tipo == "Pizza");
        if (sabor1 is null) return BadRequest("Sabor 1 inválido.");

        Produto? sabor2 = null;
        if (dto.Sabor2Id.HasValue)
        {
            sabor2 = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == dto.Sabor2Id.Value && p.Tipo == "Pizza");
            if (sabor2 is null) return BadRequest("Sabor 2 inválido.");
        }

        Produto? borda = null;
        if (dto.BordaId.HasValue)
        {
            borda = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == dto.BordaId.Value && p.Tipo == "Borda");
            if (borda is null) return BadRequest("Borda inválida.");
        }

        var adicionaisIds = dto.AdicionaisIds ?? new List<int>();
        var adicionais = await _db.Produtos
            .Where(p => adicionaisIds.Contains(p.Id) && p.Tipo == "Adicional")
            .ToListAsync();

        var precoSabor = sabor2 is null ? sabor1.Preco : Math.Max(sabor1.Preco, sabor2.Preco);
        var preco = precoSabor * tamanho.Fator;
        if (borda is not null) preco += borda.Preco;
        preco += adicionais.Sum(a => a.Preco);
        preco = Math.Round(preco, 2);

        var descricao = $"Pizza {tamanho.Nome} - {sabor1.Nome}";
        if (sabor2 is not null) descricao += $" / {sabor2.Nome}";
        if (borda is not null) descricao += $" + Borda {borda.Nome}";
        if (adicionais.Count > 0) descricao += $" + {string.Join(", ", adicionais.Select(a => a.Nome))}";

        var carrinho = await ObterOuCriarCarrinho();

        var item = new CarrinhoItem
        {
            CarrinhoId = carrinho.Id,
            Descricao = descricao,
            TamanhoId = tamanho.Id,
            Sabor1Id = sabor1.Id,
            Sabor2Id = sabor2?.Id,
            BordaId = borda?.Id,
            Quantidade = dto.Quantidade,
            PrecoUnitario = preco
        };

        foreach (var a in adicionais)
        {
            item.Adicionais.Add(new CarrinhoItemAdicional { ProdutoId = a.Id });
        }

        _db.CarrinhoItens.Add(item);
        await _db.SaveChangesAsync();

        return Ok(await MontarCarrinhoDto(carrinho.Id));
    }

    [HttpPut("itens/{id:int}")]
    public async Task<ActionResult<CarrinhoDto>> AtualizarQuantidade(int id, AtualizarQuantidadeDto dto)
    {
        if (dto.Quantidade < 1) return BadRequest("Quantidade deve ser pelo menos 1.");

        var item = await _db.CarrinhoItens.FindAsync(id);
        if (item is null) return NotFound();

        item.Quantidade = dto.Quantidade;
        await _db.SaveChangesAsync();

        return Ok(await MontarCarrinhoDto(item.CarrinhoId));
    }

    [HttpDelete("itens/{id:int}")]
    public async Task<ActionResult<CarrinhoDto>> RemoverItem(int id)
    {
        var item = await _db.CarrinhoItens.FindAsync(id);
        if (item is null) return NotFound();

        var carrinhoId = item.CarrinhoId;
        _db.CarrinhoItens.Remove(item);
        await _db.SaveChangesAsync();

        return Ok(await MontarCarrinhoDto(carrinhoId));
    }

    [HttpDelete]
    public async Task<ActionResult<CarrinhoDto>> Esvaziar()
    {
        var carrinho = await ObterOuCriarCarrinho();
        var itens = await _db.CarrinhoItens.Where(i => i.CarrinhoId == carrinho.Id).ToListAsync();
        _db.CarrinhoItens.RemoveRange(itens);
        await _db.SaveChangesAsync();

        return Ok(await MontarCarrinhoDto(carrinho.Id));
    }
}
