namespace Orenes.Models
{
    public class PedidoEntregado
    {
        public int PedidoEntregadoId { get; set; }
        public int ClienteId { get; set; }
        public string DireccionEntrega { get; set; }
        public EstadoPedido Status { get; set; }
        public Cliente Cliente { get; set; }
    }
}
