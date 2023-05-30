using Microsoft.EntityFrameworkCore;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Repository.Interfaces;

namespace Orenes.Repository.Implementaciones
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly Context _context;

        public VehiculoRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        public async Task<Vehiculo> ObtenerVehiculo(int id)
        {
            return await _context.Vehiculos.FindAsync(id);
        }

        public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo)
        {
            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(vehiculo.VehiculoId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            return vehiculo;
        }

        public async Task<bool> EliminarVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return false;
            }

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.VehiculoId == id);
        }
    }
}

