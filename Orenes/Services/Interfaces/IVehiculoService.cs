using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IVehiculoService
    {
        
        Task<IEnumerable<Vehiculo>> ObtenerVehiculos();
        Task<Vehiculo> ObtenerVehiculo(int id);
        Task<bool> ActualizarVehiculo(int id, Vehiculo vehiculo);
        Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo);
        Task<bool> EliminarVehiculo(int id);
    
}
}
