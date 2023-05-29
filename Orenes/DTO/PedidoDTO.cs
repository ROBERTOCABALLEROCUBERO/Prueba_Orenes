namespace Orenes.DTO
{
    public class PedidoDTO
    {
        public int PedidoId { get; set; }
        public int ClienteId { get; set; }
        public string Detalles { get; set; }
        public string DireccionRecogida { get; set; }
        public string DireccionEntrega { get; set; }
    }
}
