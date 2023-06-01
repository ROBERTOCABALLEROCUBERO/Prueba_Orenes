using Orenes.Models;

namespace Orenes.DTO
{
    public class PedidoDTO
    {
        public string DireccionEntrega { get; set; }

        public EstadoPedido status { get; set; }
    }
}
