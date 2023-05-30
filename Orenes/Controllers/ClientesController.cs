using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orenes.DTO;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Services.Implementaciones;
using Orenes.Services.Interfaces;

namespace Orenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ISecurityService _securityService;

        public ClientesController(IClienteService clienteService, ISecurityService securityService)
        {
            _clienteService = clienteService;
            _securityService = securityService; 
        }

        // GET: api/Clientes/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Cliente>> Login(string nombre, string password)
        {
            Cliente cliente = await _clienteService.Login(nombre);
            if (cliente != null)
            {
                bool esClaveValida = _securityService.Desencriptar(cliente.Password, password);
                if (esClaveValida)
                {
                    // Contraseña válida, realizar el login
         

                    if (cliente != null)
                    {
                        var token = _securityService.GenerarToken(cliente.Nombre);

                        // Retorna una respuesta 200 OK con el token JWT
                        return Ok(new { token });
                    }
                    else
                    {
                        // Ocurrió un error en el proceso de login
                        // Puedes devolver un resultado de error específico si lo deseas
                        return BadRequest("Error en el proceso de login");
                    }
                }
                else
                {
                    // Contraseña inválida, mostrar mensaje de error o realizar alguna acción correspondiente
                    return BadRequest("Contraseña inválida");
                }
            }
            else
            {
                // No se encontró un cliente con el nombre proporcionado, mostrar mensaje de error o realizar alguna acción correspondiente
                return NotFound("Cliente no encontrado");
            }
        }




        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.ObtenerTodosLosClientes();
            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            var updated = await _clienteService.ActualizarCliente(cliente);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Registro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> Registro(Cliente cliente)
        {

            var clienteEncriptado = new Cliente
            {
                Nombre = cliente.Nombre,
                Password = _securityService.Encriptar(cliente.Password)
            };
            var clienteId = await _clienteService.CrearCliente(clienteEncriptado);
            cliente.ClienteId = clienteId;

            return CreatedAtAction(nameof(GetCliente), new { id = clienteId }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var deleted = await _clienteService.EliminarCliente(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        // GET: api/Clientes/ObtenerDatosUsuario
        [HttpGet("ObtenerDatosUsuario")]
        public async Task<ActionResult<Cliente>> ObtenerDatosUsuario()
        {
            try
            {
                // Obtener el token del encabezado de la solicitud


                // Decodificar el token y obtener el valor del nombre del usuario
                string usuario = User.FindFirst(ClaimTypes.Name).Value;


                // Obtener los datos del usuario a través del servicio
                Cliente cliente = await _clienteService.ObtenerDatosUsuarioPorNombre(usuario);

                if (cliente != null)
                {
                    return Ok(cliente);
                }
                else
                {
                    return NotFound("Cliente no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los datos del usuario: " + ex.Message);
            }
        }


    }

}
