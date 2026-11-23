namespace Pizzaria.Api.Dtos;

public record FinalizarPedidoDto(int UsuarioId, string Endereco, string FormaPagamento);

public record PedidoItemDto(string Descricao, int Quantidade, decimal PrecoUnitario, decimal Subtotal);

public record PedidoDto(
    int Id,
    DateTime CriadoEm,
    string Status,
    string Endereco,
    string FormaPagamento,
    decimal Total,
    List<PedidoItemDto> Itens
);

public record AtualizarStatusDto(string Status);

public record PedidoAdminDto(
    int Id,
    DateTime CriadoEm,
    string Cliente,
    string Status,
    string Endereco,
    string FormaPagamento,
    decimal Total,
    List<PedidoItemDto> Itens
);
