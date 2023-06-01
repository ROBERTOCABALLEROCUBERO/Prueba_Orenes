using Newtonsoft.Json;

namespace Orenes.Models
{
    public enum EstadoPedido
    {
        Pendiente,
        EnProceso,
        Entregado
    }
    public class Pedido
    {
        public ICollection<Ubicacion> Ubicaciones { get; set; }

        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string DireccionEntrega { get; set; }

        public EstadoPedido status { get; set; }
        
        
        [JsonIgnore]
        public Cliente Cliente { get; set; }

    }
}
