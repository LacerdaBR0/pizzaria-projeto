namespace Pizzaria.Api.Models;

public class CarrinhoItem
{
    public int Id { get; set; }
    public int CarrinhoId { get; set; }
    public Carrinho? Carrinho { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int TamanhoId { get; set; }
    public Tamanho? Tamanho { get; set; }
    public int Sabor1Id { get; set; }
    public Produto? Sabor1 { get; set; }
    public int? Sabor2Id { get; set; }
    public Produto? Sabor2 { get; set; }
    public int? BordaId { get; set; }
    public Produto? Borda { get; set; }
    public int Quantidade { get; set; } = 1;
    public decimal PrecoUnitario { get; set; }
    public ICollection<CarrinhoItemAdicional> Adicionais { get; set; } = new List<CarrinhoItemAdicional>();
}
