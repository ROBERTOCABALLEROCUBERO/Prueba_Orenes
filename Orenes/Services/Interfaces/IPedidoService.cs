using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ObtenerPedidos();
        Task<Pedido> ObtenerPedido(int pedidoId);
        Task<int> CrearPedido(PedidoDTO pedido, Cliente cliente);
        Task<bool> ActualizarPedido(Pedido pedido);
        Task<bool> EliminarPedido(int pedidoId);
        Task<bool> Pedidoentregado(PedidoEntregado pedidoEntregado);
    }
}
