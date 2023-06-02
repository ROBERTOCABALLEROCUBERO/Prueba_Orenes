using Microsoft.EntityFrameworkCore;
using Orenes.DTO;
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
        public async Task<List<PedidoDTO>> ObtenerPedidosPorVehiculo(int vehiculoId)
        {
            // Implementa la lógica para obtener los pedidos por vehículo desde el DbContext
            // y mapearlos a una lista de PedidoDTO
            var pedidos = await _context.Pedidos
                .Where(p => p.VehiculoId1 == vehiculoId)
                .ToListAsync();

            var pedidosDTO = pedidos.Select(p => new PedidoDTO
            {
                // Mapea las propiedades relevantes de Pedido a PedidoDTO
                PedidoId = p.PedidoId,
                DireccionEntrega = p.DireccionEntrega,
                status = p.status
                
               
                
                // Otras propiedades...
            }).ToList();

            return pedidosDTO;
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
        public async Task<List<Pedido>> ObtenerPedidosPorIdUsuario(int idUsuario)
        {
            return await _context.Pedidos
                      .Where(p => p.ClienteId == idUsuario)
                      .ToListAsync();
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
