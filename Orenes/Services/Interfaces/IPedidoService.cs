using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ObtenerPedidos();
        Task<List<PedidoDTO>> ObtenerPedidosPorIdUsuario(int idUsuario);
        Task<List<PedidoDTO>> ObtenerPedidosPorVehiculo(int vehiculoId);

        Task<Pedido> ObtenerPedido(int pedidoId);
        Task<int> CrearPedido(PedidoDTO pedido, Cliente cliente);
        Task<bool> ActualizarPedido(Pedido pedido);
        Task<bool> EliminarPedido(Pedido pedido);
        Task<bool> Pedidoentregado(PedidoEntregado pedidoEntregado);
    }
}
