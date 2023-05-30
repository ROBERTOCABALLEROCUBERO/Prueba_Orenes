using Microsoft.EntityFrameworkCore;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Repository.Interfaces;

namespace Orenes.Repository.Implementaciones
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly Context _context;

        public PedidoRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> ObtenerTodosLosPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido> ObtenerPedidoPorId(int pedidoId)
        {
            return await _context.Pedidos.FindAsync(pedidoId);
        }

        public async Task<int> CrearPedido(Pedido pedido)
        {
            
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.PedidoId;
        }

        public async Task<bool> ActualizarPedido(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPedido(Pedido pedido)
        {
            if (pedido == null) { 
                return false;
        }
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CrearPedidoEntregado(PedidoEntregado pedido)
        {
            try
            {
                _context.PedidoEntregado.Add(pedido);
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                throw new Exception("Error al crear el pedido entregado", ex);
            }
        }

    }
}
