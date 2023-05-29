namespace Orenes.Models
{
    public class Vehiculo
    {
        public int VehiculoId { get; set; }
        public decimal UbicacionLat { get; set; }
        public decimal UbicacionLon { get; set; }
        public ICollection<Ubicacion> Ubicaciones { get; set; }


    }
}
