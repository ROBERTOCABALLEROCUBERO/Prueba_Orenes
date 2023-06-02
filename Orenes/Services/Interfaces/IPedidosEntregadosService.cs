using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IPedidosEntregadosService
    {

        Task<List<PedidoEntregado>> ObtenerTodosLosPE();
        Task<PedidoEntregado> ObtenerPEPorId(int id);
        Task<PedidoEntregado> CrearPE(PedidoEntregado pedidoEntregado);
        Task<PedidoEntregado> ActualizarPE(int id, PedidoEntregado pedidoEntregado);
        Task<bool> EliminarPE(int id);
        Task<List<PedidoEntregado>> ObtenerPorClienteId(int clienteId);

    }
}

