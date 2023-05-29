namespace Orenes.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string Detalles { get; set; }
        public string DireccionEntrega { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Ubicacion> Ubicaciones { get; set; }
    }
}
