using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IUbicacionRepository
    {

        Task<IEnumerable<Ubicacion>> ObtenerUbicaciones();
        Task<Ubicacion> ObtenerUbicacion(int id);
        Task<bool> ActualizarUbicacion(int id, Ubicacion ubicacion);
        Task<Ubicacion> AgregarUbicacion(Ubicacion ubicacion);
        Task<bool> EliminarUbicacion(int id);
    }
}
