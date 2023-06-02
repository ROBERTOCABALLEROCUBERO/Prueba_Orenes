using Orenes.DTO;
using Orenes.Models;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;
using System.Security.Claims;

namespace Orenes.Services.Implementaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<Pedido>> ObtenerPedidos()
        {
            return await _pedidoRepository.ObtenerTodosLosPedidos();
        }
        public async Task<List<PedidoDTO>> ObtenerPedidosPorIdUsuario(int idUsuario)
        {
            var pedidos = await _pedidoRepository.ObtenerPedidosPorIdUsuario(idUsuario);

            var pedidosDTO = pedidos.Select(p => new PedidoDTO
            {
                PedidoId = p.PedidoId,
                DireccionEntrega = p.DireccionEntrega,
                status = p.status
            }).ToList();

            return pedidosDTO;
        }

        public async Task<List<PedidoDTO>> ObtenerPedidosPorVehiculo(int vehiculoId)
        {
            return await _pedidoRepository.ObtenerPedidosPorVehiculo(vehiculoId);
        }



        public async Task<Pedido> ObtenerPedido(int pedidoId)
        {
            return await _pedidoRepository.ObtenerPedidoPorId(pedidoId);
        }

        public async Task<int> CrearPedido(PedidoDTO pedidodto, Cliente cliente)
        {



            var pedido = new Pedido {
                Cliente = cliente,
                status = EstadoPedido.Pendiente,
                ClienteId = cliente.ClienteId,
                DireccionEntrega = pedidodto.DireccionEntrega,
                VehiculoId1 = null

        };
            return await _pedidoRepository.CrearPedido(pedido);
        }

        public async Task<bool> ActualizarPedido(Pedido pedido)
        {
            return await _pedidoRepository.ActualizarPedido(pedido);
        }

        public async Task<bool> EliminarPedido(Pedido pedido)
        {
            return await _pedidoRepository.EliminarPedido(pedido);
        }

        public async Task<bool> Pedidoentregado(PedidoEntregado pedidoEntregado)
        {
            return await _pedidoRepository.CrearPedidoEntregado(pedidoEntregado);
        }
    }
}
