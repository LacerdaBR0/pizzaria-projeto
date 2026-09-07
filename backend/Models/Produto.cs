namespace Pizzaria.Api.Models;

public class Produto
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Tipo { get; set; } = "Pizza";
    public decimal Preco { get; set; }
    public bool Ativo { get; set; } = true;
}
