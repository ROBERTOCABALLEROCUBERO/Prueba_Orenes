using Microsoft.EntityFrameworkCore;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orenes.Repository.Implementaciones
{
    public class PedidosEntregadosRepository : IPedidosEntregadosRepository
    {
        private readonly Context _context;

        public PedidosEntregadosRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PedidoEntregado>> ObtenerTodos()
        {
            // Obtener todos los pedidos entregados
            return await _context.PedidoEntregado.ToListAsync();
        }

        public async Task<PedidoEntregado> ObtenerPorId(int pedidoId)
        {
            // Obtener un pedido entregado por su ID
            return await _context.PedidoEntregado.FindAsync(pedidoId);
        }

        public async Task Crear(PedidoEntregado pedidoEntregado)
        {
            // Crear un nuevo pedido entregado
            await _context.PedidoEntregado.AddAsync(pedidoEntregado);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(PedidoEntregado pedidoEntregado)
        {
            // Actualizar un pedido entregado existente
            _context.PedidoEntregado.Update(pedidoEntregado);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int pedidoId)
        {
            // Eliminar un pedido entregado por su ID
            var pedidoEntregado = await _context.PedidoEntregado.FindAsync(pedidoId);
            if (pedidoEntregado != null)
            {
                _context.PedidoEntregado.Remove(pedidoEntregado);
                await _context.SaveChangesAsync();
            }
        }
    }
}

