using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IVehiculoRepository
    {

        Task<IEnumerable<Vehiculo>> ObtenerVehiculos();
        Task<Vehiculo> ObtenerVehiculo(int id);
        Task<bool> ActualizarVehiculo(Vehiculo vehiculo);
        Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo);
        Task<bool> EliminarVehiculo(int id);
    }
}
