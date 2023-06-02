using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> ObtenerTodosLosPedidos();
        Task<Pedido> ObtenerPedidoPorId(int pedidoId);
        Task<int> CrearPedido(Pedido pedido);
        Task<bool> ActualizarPedido(Pedido pedido);
        Task<bool> EliminarPedido(Pedido pedido);
        Task<List<Pedido>> ObtenerPedidosPorIdUsuario(int idUsuario);

        Task<List<PedidoDTO>> ObtenerPedidosPorVehiculo(int vehiculoId);
        Task<bool> CrearPedidoEntregado(PedidoEntregado pedido);
    }
}
