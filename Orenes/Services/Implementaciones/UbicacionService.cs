using Orenes.DTO;
using Orenes.Models;
using Orenes.Repository.Implementaciones;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;

namespace Orenes.Services.Implementaciones
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _ubicacionesRepository;

        public UbicacionService(IUbicacionRepository ubicacionesRepository)
        {
            _ubicacionesRepository = ubicacionesRepository;
        }

        public async Task<IEnumerable<Ubicacion>> ObtenerUbicaciones()
        {
            var ubicaciones = await _ubicacionesRepository.ObtenerUbicaciones();
            return ubicaciones;
        }

        public async Task<Ubicacion> ObtenerUbicacion(int id)
        {
            var ubicacion = await _ubicacionesRepository.ObtenerUbicacion(id);
            return ubicacion;
        }

        public async Task<bool> ActualizarUbicacion(int id, Ubicacion ubicacion)
        {
            var resultado = await _ubicacionesRepository.ActualizarUbicacion(id, ubicacion);
            return resultado;
        }

        public async Task<UbicacionDTO> AgregarUbicacion(UbicacionDTO ubicacion)
        {
           
            var nuevaUbicacion = await _ubicacionesRepository.AgregarUbicacion(ubicacion);
            return nuevaUbicacion;
        }

        public async Task<bool> EliminarUbicacion(int id)
        {
            var resultado = await _ubicacionesRepository.EliminarUbicacion(id);
            return resultado;
        }
        public async Task<Ubicacion> ObtenerUbicacionMasReciente(
             int pedidoId)
        {
            return await _ubicacionesRepository.ObtenerUbicacionMasReciente(pedidoId);
        }
    }
}

