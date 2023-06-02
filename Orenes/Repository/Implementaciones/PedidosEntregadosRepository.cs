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

        public async Task<List<PedidoEntregado>> ObtenerTodosLosPE()
        {
            return await _context.PedidoEntregado.ToListAsync();
        }

        public async Task<PedidoEntregado> ObtenerPEPorId(int id)
        {
            return await _context.PedidoEntregado.FindAsync(id);
        }

        public async Task<PedidoEntregado> CrearPE(PedidoEntregado pedidoEntregado)
        {
            _context.PedidoEntregado.Add(pedidoEntregado);
            await _context.SaveChangesAsync();
            return pedidoEntregado;
        }

        public async Task<PedidoEntregado> ActualizarPE(int id, PedidoEntregado pedidoEntregado)
        {
            var peExistente = await _context.PedidoEntregado.FindAsync(id);
            if (peExistente == null)
                return null;

            await _context.SaveChangesAsync();
            return peExistente;
        }

        public async Task<bool> EliminarPE(int id)
        {
            var peExistente = await _context.PedidoEntregado.FindAsync(id);
            if (peExistente == null)
                return false;

            _context.PedidoEntregado.Remove(peExistente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PedidoEntregado>> ObtenerPorClienteId(int clienteId)
        {
            return await _context.PedidoEntregado.Where(pe => pe.ClienteId == clienteId).ToListAsync();
        }
    }

  
}
