using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProdutosController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoReadDto>>> Listar()
    {
        var produtos = await _db.Produtos
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nome)
            .Select(p => new ProdutoReadDto(
                p.Id,
                p.CategoriaId,
                p.Categoria!.Nome,
                p.Nome,
                p.Descricao,
                p.Tipo,
                p.Preco,
                p.Ativo))
            .ToListAsync();

        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoReadDto>> Obter(int id)
    {
        var p = await _db.Produtos.Include(x => x.Categoria).FirstOrDefaultAsync(x => x.Id == id);
        if (p is null) return NotFound();

        return Ok(new ProdutoReadDto(
            p.Id, p.CategoriaId, p.Categoria!.Nome, p.Nome, p.Descricao, p.Tipo, p.Preco, p.Ativo));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoReadDto>> Criar(ProdutoCreateDto dto)
    {
        var categoriaExiste = await _db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste) return BadRequest("Categoria inexistente.");

        var produto = new Produto
        {
            CategoriaId = dto.CategoriaId,
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            Tipo = dto.Tipo,
            Preco = dto.Preco,
            Ativo = dto.Ativo
        };

        _db.Produtos.Add(produto);
        await _db.SaveChangesAsync();

        await _db.Entry(produto).Reference(x => x.Categoria).LoadAsync();

        var read = new ProdutoReadDto(
            produto.Id, produto.CategoriaId, produto.Categoria!.Nome,
            produto.Nome, produto.Descricao, produto.Tipo, produto.Preco, produto.Ativo);

        return CreatedAtAction(nameof(Obter), new { id = produto.Id }, read);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, ProdutoUpdateDto dto)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        var categoriaExiste = await _db.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste) return BadRequest("Categoria inexistente.");

        produto.CategoriaId = dto.CategoriaId;
        produto.Nome = dto.Nome;
        produto.Descricao = dto.Descricao;
        produto.Tipo = dto.Tipo;
        produto.Preco = dto.Preco;
        produto.Ativo = dto.Ativo;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var produto = await _db.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        _db.Produtos.Remove(produto);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
