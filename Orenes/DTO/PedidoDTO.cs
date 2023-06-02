using Orenes.Models;

namespace Orenes.DTO
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }
        public string DireccionEntrega { get; set; }

        public EstadoPedido status { get; set; }
    }
}
