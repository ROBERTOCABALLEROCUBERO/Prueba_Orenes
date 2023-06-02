using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IPedidosEntregadosRepository
    {

        Task<List<PedidoEntregado>> ObtenerTodosLosPE();
        Task<PedidoEntregado> ObtenerPEPorId(int id);
        Task<PedidoEntregado> CrearPE(PedidoEntregado pedidoEntregado);
        Task<PedidoEntregado> ActualizarPE(int id, PedidoEntregado pedidoEntregado);
        Task<bool> EliminarPE(int id);
        Task<List<PedidoEntregado>> ObtenerPorClienteId(int clienteId);

    }
}
