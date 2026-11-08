namespace Pizzaria.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public string FormaPagamento { get; set; } = string.Empty;
    public string Status { get; set; } = "Recebido";
    public decimal Total { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
}
