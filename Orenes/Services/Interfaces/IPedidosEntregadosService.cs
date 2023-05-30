using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IPedidosEntregadosService
    {
      
            Task<List<PedidoEntregado>> ObtenerTodosLosPE();
            Task CrearPedidoEntregado(PedidoEntregado pedidoEntregado);
            Task ActualizarPedidoEntregado(PedidoEntregado pedidoEntregado);
            Task EliminarPedidoEntregado(int pedidoId);
        }
}

