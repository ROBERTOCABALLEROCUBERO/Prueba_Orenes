using Microsoft.EntityFrameworkCore;
using Orenes.Models;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orenes.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculosRepository;

        public VehiculoService(IVehiculoRepository vehiculosRepository)
        {
            _vehiculosRepository = vehiculosRepository;
        }

        public async Task<IEnumerable<Vehiculo>> ObtenerVehiculos()
        {
            return await _vehiculosRepository.ObtenerVehiculos();
        }

        public async Task<Vehiculo> ObtenerVehiculo(int id)
        {
            return await _vehiculosRepository.ObtenerVehiculo(id);
        }

        public async Task<bool> ActualizarVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.VehiculoId)
            {
                return false;
            }

            return await _vehiculosRepository.ActualizarVehiculo(vehiculo);
        }

        public async Task<Vehiculo> AgregarVehiculo(Vehiculo vehiculo)
        {
            return await _vehiculosRepository.AgregarVehiculo(vehiculo);
        }

        public async Task<bool> EliminarVehiculo(int id)
        {
            return await _vehiculosRepository.EliminarVehiculo(id);
        }
    }
}

