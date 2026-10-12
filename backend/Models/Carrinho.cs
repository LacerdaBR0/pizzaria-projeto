namespace Pizzaria.Api.Models;

public class Carrinho
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public ICollection<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
}
