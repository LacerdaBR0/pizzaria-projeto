namespace Pizzaria.Api.Dtos;

public record ContagemStatusDto(string Status, int Quantidade);

public record MaisVendidaDto(string Descricao, int Quantidade);

public record RelatorioDto(
    decimal TotalFaturado,
    int TotalPedidos,
    decimal TicketMedio,
    List<ContagemStatusDto> PorStatus,
    List<MaisVendidaDto> MaisVendidas
);
