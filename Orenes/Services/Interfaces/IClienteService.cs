using Orenes.DTO;
using Orenes.Models;

namespace Orenes.Services.Interfaces
{
    public interface IClienteService
    {

        public Task<Cliente> Login(string nombre);
        public Task<Cliente> ObtenerClientePorId(int clienteId);
        Task<List<Cliente>> ObtenerTodosLosClientes();
        Task<Cliente> ObtenerDatosUsuarioPorNombre(string nombreUsuario);
        Task<int> CrearCliente(Cliente cliente);
        Task<bool> ActualizarCliente(Cliente cliente);
        Task<bool> EliminarCliente(int clienteId);
    }
}
