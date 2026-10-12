namespace Pizzaria.Api.Models;

public class Tamanho
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int? Fatias { get; set; }
    public decimal Fator { get; set; } = 1m;
}
