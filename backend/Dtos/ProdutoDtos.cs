namespace Pizzaria.Api.Dtos;

public record ProdutoCreateDto(
    int CategoriaId,
    string Nome,
    string? Descricao,
    string Tipo,
    decimal Preco,
    bool Ativo
);

public record ProdutoUpdateDto(
    int CategoriaId,
    string Nome,
    string? Descricao,
    string Tipo,
    decimal Preco,
    bool Ativo
);

public record ProdutoReadDto(
    int Id,
    int CategoriaId,
    string CategoriaNome,
    string Nome,
    string? Descricao,
    string Tipo,
    decimal Preco,
    bool Ativo
);

public record CategoriaDto(int Id, string Nome, bool Ativo);
