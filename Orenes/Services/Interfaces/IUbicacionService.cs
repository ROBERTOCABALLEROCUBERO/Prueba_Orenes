using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IUbicacionService
    {

        Task<IEnumerable<Ubicacion>> ObtenerUbicaciones();
        Task<Ubicacion> ObtenerUbicacion(int id);
        Task<bool> ActualizarUbicacion(int id, Ubicacion ubicacion);
        Task<Ubicacion> AgregarUbicacion(Ubicacion ubicacion);
        Task<bool> EliminarUbicacion(int id);
    }
}
