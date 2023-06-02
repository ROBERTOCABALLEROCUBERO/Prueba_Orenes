using Microsoft.EntityFrameworkCore;
using Orenes.DTO;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Repository.Interfaces;

namespace Orenes.Repository.Implementaciones
{
    public class UbicacionRepository : IUbicacionRepository
    {

        private readonly Context _context;

        public UbicacionRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ubicacion>> ObtenerUbicaciones()
        {
            return await _context.Ubicaciones.ToListAsync();
        }

        public async Task<Ubicacion> ObtenerUbicacion(int id)
        {
            return await _context.Ubicaciones.FindAsync(id);
        }

        public async Task<bool> ActualizarUbicacion(int id, Ubicacion ubicacion)
        {
            var ubicacionExistente = await _context.Ubicaciones.FindAsync(id);

            if (ubicacionExistente == null)
            {
                return false;
            }


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UbicacionDTO> AgregarUbicacion(UbicacionDTO ubicacion)
        {
            
            _context.Ubicaciones.Add(new Ubicacion
            {
                UbicacionId = ubicacion.UbicacionId,
                VehiculoId = ubicacion.VehiculoId,
                PedidoId = ubicacion.PedidoId,
                Latitud = ubicacion.Latitud,
                Longitud = ubicacion.Longitud,
                FechaHora = ubicacion.FechaHora
            });
            await _context.SaveChangesAsync();

            return ubicacion;
        }

        public async Task<bool> EliminarUbicacion(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);

            if (ubicacion == null)
            {
                return false;
            }

            _context.Ubicaciones.Remove(ubicacion);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Ubicacion> ObtenerUbicacionMasReciente(int pedidoId)
        {
            return await _context.Ubicaciones
                .Where(u => u.PedidoId == pedidoId)
                .OrderByDescending(u => u.FechaHora)
                .FirstOrDefaultAsync();
        }
    }
}

