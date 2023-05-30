using Orenes.Models;

namespace Orenes.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> Login(string nombre);
        Task<Cliente> ObtenerClientePorId(int clienteId);
        Task<List<Cliente>> ObtenerTodosLosClientes();
        Task<int> CrearCliente(Cliente cliente);
        Task<bool> ActualizarCliente(Cliente cliente);
        Task<bool> EliminarCliente(int clienteId);
        Task<Cliente> ObtenerDatosUsuarioPorNombre(string nombreUsuario);


    }
}
