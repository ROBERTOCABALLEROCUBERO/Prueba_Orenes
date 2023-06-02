using Orenes.Models;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;

namespace Orenes.Services.Implementaciones
{
    public class PedidosEntregadosService : IPedidosEntregadosService
    {
        private readonly IPedidosEntregadosRepository _pedidoEntregadosRepository;


        public PedidosEntregadosService(IPedidosEntregadosRepository pedidoEntregadosRepository)
        {
            _pedidoEntregadosRepository = pedidoEntregadosRepository;
        }

        public async Task<List<PedidoEntregado>> ObtenerTodosLosPE()
        {
            return await _pedidoEntregadosRepository.ObtenerTodosLosPE();
        }

        public async Task<PedidoEntregado> ObtenerPEPorId(int id)
        {
            return await _pedidoEntregadosRepository.ObtenerPEPorId(id);
        }
        public async Task<List<PedidoEntregado>> ObtenerPorClienteId(int clienteId)
        {
            return await _pedidoEntregadosRepository.ObtenerPorClienteId(clienteId);

        }
        public async Task<PedidoEntregado> CrearPE(PedidoEntregado pedidoEntregado)
        {
            return await _pedidoEntregadosRepository.CrearPE(pedidoEntregado);
        }

        public async Task<PedidoEntregado> ActualizarPE(int id, PedidoEntregado pedidoEntregado)
        {
            return await _pedidoEntregadosRepository.ActualizarPE(id, pedidoEntregado);
        }

        public async Task<bool> EliminarPE(int id)
        {
            return await _pedidoEntregadosRepository.EliminarPE(id);
        }


    }
}

