namespace Orenes.Models
{
    public class Ubicacion
    {
        public int UbicacionId { get; set; }
        public int VehiculoId { get; set; }
        public int PedidoId { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime FechaHora { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public Pedido Pedido { get; set; }
    }
}
