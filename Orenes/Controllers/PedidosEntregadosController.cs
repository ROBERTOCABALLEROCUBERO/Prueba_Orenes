using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orenes.Models;
using Orenes.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosEntregadosController : ControllerBase
    {
        private readonly IPedidosEntregadosService _pedidoEntregadosService;
        private readonly IClienteService _clienteService;


        public PedidosEntregadosController(IPedidosEntregadosService pedidoEntregadosService, IClienteService clienteService)
        {
            _pedidoEntregadosService = pedidoEntregadosService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoEntregado>>> ObtenerTodosLosPE()
        {
            try
            {
                var pedidosEntregados = await _pedidoEntregadosService.ObtenerTodosLosPE();
                return Ok(pedidosEntregados);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al obtener los pedidos entregados.");
            }
        }

        [Authorize]
        [HttpGet("obtenercliente")]
        public async Task<ActionResult<List<PedidoEntregado>>> Obtenerporcliente()
        {
            try
            {
                string nombre = User.FindFirst(ClaimTypes.Name).Value;

                Cliente usuario = await _clienteService.ObtenerDatosUsuarioPorNombre(nombre);

                var pedidosEntregados = await _pedidoEntregadosService.ObtenerPorClienteId(usuario.ClienteId);
                return Ok(pedidosEntregados);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al obtener los pedidos entregados del cliente.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoEntregado>> ObtenerPEPorId(int id)
        {
            try
            {
                var pedidoEntregado = await _pedidoEntregadosService.ObtenerPEPorId(id);
                if (pedidoEntregado == null)
                    return NotFound();

                return Ok(pedidoEntregado);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al obtener el pedido entregado.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PedidoEntregado>> CrearPE(PedidoEntregado pedidoEntregado)
        {
            try
            {
                var nuevoPedidoEntregado = await _pedidoEntregadosService.CrearPE(pedidoEntregado);
                return CreatedAtAction(nameof(ObtenerPEPorId), new { id = nuevoPedidoEntregado.PedidoEntregadoId }, nuevoPedidoEntregado);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al crear el pedido entregado.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PedidoEntregado>> ActualizarPE(int id, PedidoEntregado pedidoEntregado)
        {
            try
            {
                var pedidoActualizado = await _pedidoEntregadosService.ActualizarPE(id, pedidoEntregado);
                if (pedidoActualizado == null)
                    return NotFound();

                return Ok(pedidoActualizado);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al actualizar el pedido entregado.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> EliminarPE(int id)
        {
            try
            {
                var resultado = await _pedidoEntregadosService.EliminarPE(id);
                if (!resultado)
                    return NotFound();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Manejar el error según tus necesidades
                return StatusCode(500, "Ocurrió un error al eliminar el pedido entregado.");
            }
        }
    }
}
