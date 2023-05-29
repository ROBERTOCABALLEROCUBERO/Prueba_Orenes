using Microsoft.EntityFrameworkCore;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Repository.Interfaces;
using System.Data.SqlClient;

namespace Orenes.Repository.Implementaciones
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;

        public ClienteRepository(Context context)
        {
            _context = context;
        }

      
        public async Task<Cliente> Login(string nombre)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Nombre == nombre);
        }
        public async Task<Cliente> ObtenerClientePorId(int clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }

        public async Task<List<Cliente>> ObtenerTodosLosClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<int> CrearCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente.ClienteId;
            }
            catch (DbUpdateException ex)
            {
                // Verificar si la excepción está relacionada con la restricción de unicidad
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    // El campo Nombre ya existe, devolver un resultado indicando el error
                    return -1; // O cualquier otro valor o indicador de error que desees usar
                }

                // Si la excepción no está relacionada con la restricción de unicidad, relanzarla
                throw;
            }
        }


        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            var existingCliente = await _context.Clientes.FindAsync(cliente.ClienteId);
            if (existingCliente == null)
            {
                return false; 
            }

            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Password = cliente.Password;

            _context.Entry(existingCliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarCliente(int clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                return false;
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
