namespace Pizzaria.Api.Models;

public class CarrinhoItemAdicional
{
    public int Id { get; set; }
    public int CarrinhoItemId { get; set; }
    public CarrinhoItem? CarrinhoItem { get; set; }
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
}
