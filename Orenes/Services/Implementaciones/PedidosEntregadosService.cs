using Orenes.Models;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;

namespace Orenes.Services.Implementaciones
{
    public class PedidosEntregadosService : IPedidosEntregadosService
    {
        private readonly IPedidosEntregadosRepository _pedidosEntregadosRepository;

        public PedidosEntregadosService(IPedidosEntregadosRepository pedidosEntregadosRepository)
        {
            _pedidosEntregadosRepository = pedidosEntregadosRepository;
        }

        public async Task<List<PedidoEntregado>> ObtenerTodosLosPE()
        {
            return await _pedidosEntregadosRepository.ObtenerTodos();
        }

        public async Task CrearPedidoEntregado(PedidoEntregado pedidoEntregado)
        {
            await _pedidosEntregadosRepository.Crear(pedidoEntregado);
        }

        public async Task ActualizarPedidoEntregado(PedidoEntregado pedidoEntregado)
        {
            await _pedidosEntregadosRepository.Actualizar(pedidoEntregado);
        }

        public async Task EliminarPedidoEntregado(int pedidoId)
        {
            await _pedidosEntregadosRepository.Eliminar(pedidoId);
        }
    }


}

