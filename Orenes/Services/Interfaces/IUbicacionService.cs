using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IUbicacionService
    {

        Task<IEnumerable<Ubicacion>> ObtenerUbicaciones();
        Task<Ubicacion> ObtenerUbicacion(int id);
        Task<bool> ActualizarUbicacion(int id, Ubicacion ubicacion);
        Task<UbicacionDTO> AgregarUbicacion(UbicacionDTO ubicacion);
        Task<bool> EliminarUbicacion(int id);
        Task<Ubicacion> ObtenerUbicacionMasReciente(int pedidoId);

    }
}
