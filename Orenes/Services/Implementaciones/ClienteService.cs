using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Orenes.DTO;
using Orenes.Models;
using Orenes.Repository.Implementaciones;
using Orenes.Repository.Interfaces;
using Orenes.Services.Interfaces;

namespace Orenes.Services.Implementaciones
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
       

        public ClienteService(IClienteRepository clienteRepository, ISecurityService securityService)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> Login(string nombre)
        {
            return await _clienteRepository.Login(nombre);
        }

        public async Task<Cliente> ObtenerClientePorId(int clienteId)
        {
            return await _clienteRepository.ObtenerClientePorId(clienteId);
        }
        public async Task<List<Cliente>> ObtenerTodosLosClientes()
        {
            return await _clienteRepository.ObtenerTodosLosClientes();
        }
        public async Task<int> CrearCliente(Cliente cliente)
        {
            
            return await _clienteRepository.CrearCliente(cliente);
        }

        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            
            
            return await _clienteRepository.ActualizarCliente(cliente);
        }

        public async Task<bool> EliminarCliente(int clienteId)
        {
            return await _clienteRepository.EliminarCliente(clienteId);
        }

   
    }

}
