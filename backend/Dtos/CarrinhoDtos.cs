namespace Pizzaria.Api.Dtos;

public record MontarItemDto(
    int TamanhoId,
    int Sabor1Id,
    int? Sabor2Id,
    int? BordaId,
    List<int>? AdicionaisIds,
    int Quantidade
);

public record AtualizarQuantidadeDto(int Quantidade);

public record CarrinhoItemDto(
    int Id,
    string Descricao,
    string Tamanho,
    int Quantidade,
    decimal PrecoUnitario,
    decimal Subtotal
);

public record CarrinhoDto(
    int Id,
    List<CarrinhoItemDto> Itens,
    decimal Total
);

public record TamanhoDto(int Id, string Nome, int? Fatias, decimal Fator);
