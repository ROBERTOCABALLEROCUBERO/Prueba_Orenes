using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IPedidosEntregadosRepository
    {

        Task<List<PedidoEntregado>> ObtenerTodos();
        Task<PedidoEntregado> ObtenerPorId(int pedidoId);
        Task Crear(PedidoEntregado pedidoEntregado);
        Task Actualizar(PedidoEntregado pedidoEntregado);
        Task Eliminar(int pedidoId);
    }
}
