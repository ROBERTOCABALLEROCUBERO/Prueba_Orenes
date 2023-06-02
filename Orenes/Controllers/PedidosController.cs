using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orenes.DTO;
using Orenes.Mapping;
using Orenes.Models;
using Orenes.Services.Interfaces;
using Newtonsoft.Json;

namespace Orenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;

        public PedidosController(IPedidoService pedidoService, IClienteService clienteService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> ObtenerPedidosGeneral()
        {
            // Obtiene todos los pedidos disponibles
            var pedidos = await _pedidoService.ObtenerPedidos();
            return Ok(pedidos);
        }

        // GET: api/Pedidos/{pedidoId}
        [HttpGet("{pedidoId}")]
        public async Task<ActionResult<Pedido>> ObtenerPedido(int pedidoId)
        {
            // Obtiene un pedido específico por su ID
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        // GET: api/Pedidos/PedidosCliente
        [Authorize]
        [HttpGet("PedidosCliente")]
        public async Task<ActionResult<List<PedidoDTO>>> PedidosCliente()
        {
            // Obtiene los pedidos de un cliente específico
            string usuario = User.FindFirst(ClaimTypes.Name).Value;

            var cliente = await _clienteService.ObtenerDatosUsuarioPorNombre(usuario);
            var pedidos = await _pedidoService.ObtenerPedidosPorIdUsuario(cliente.ClienteId);

            return Ok(pedidos);
        }

        // GET: api/Pedidos/PedidosVehiculo
        [HttpGet("PedidosVehiculo")]
        public async Task<ActionResult<List<PedidoDTO>>> PedidosVehiculo(int vehiculoId)
        {
            // Obtiene los pedidos asignados a un vehículo específico
            var pedidos = await _pedidoService.ObtenerPedidosPorVehiculo(vehiculoId);

            return Ok(pedidos);
        }

        // POST: api/Pedidos
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Pedido>> CrearPedido(PedidoDTO pedido)
        {
            // Crea un nuevo pedido asociado a un cliente
            string usuario = User.FindFirst(ClaimTypes.Name).Value;
            var cliente = await _clienteService.ObtenerDatosUsuarioPorNombre(usuario);
            var pedidoId = await _pedidoService.CrearPedido(pedido, cliente);
            return CreatedAtAction(nameof(ObtenerPedido), new { pedidoId }, pedido);
        }

        // POST: api/Pedidos/MarcarEnProceso
        [HttpPost("MarcarEnProceso")]
        public async Task<IActionResult> MarcarPedidoEnProceso(int pedidoId, int vehiculoId)
        {
            // Marca un pedido como "EnProceso" y lo asigna a un vehículo
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);

            if (pedido != null)
            {
                pedido.status = EstadoPedido.EnProceso;
                pedido.VehiculoId1 = vehiculoId;

                await _pedidoService.ActualizarPedido(pedido);

                return Ok();
            }

            return NotFound();
        }

        // PUT: api/Pedidos/{pedidoId}
        [HttpPut("{pedidoId}")]
        public async Task<IActionResult> ActualizarPedido(int pedidoId, Pedido pedido)
        {
            // Actualiza un pedido existente por su ID
            if (pedidoId != pedido.PedidoId)
                return BadRequest();

            var resultado = await _pedidoService.ActualizarPedido(pedido);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Pedidos/{pedidoId}
        [HttpDelete("{pedidoId}")]
        public async Task<IActionResult> EliminarPedido(int pedidoId)
        {
            // Elimina un pedido existente por su ID
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);
            var resultado = await _pedidoService.EliminarPedido(pedido);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Pedidos/MarcarEntregado
        [HttpPost("MarcarEntregado")]
        public async Task<IActionResult> MarcarPedidoComoEntregado(int pedidoId)
        {
            // Marca un pedido como "Entregado" y lo mueve a la tabla de "PedidoEntregado"
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);

            if (pedido != null)
            {
                var pedidoEntregado = new PedidoEntregado
                {
                    ClienteId = pedido.ClienteId,
                    DireccionEntrega = pedido.DireccionEntrega,
                    Status = EstadoPedido.Entregado,
                    Cliente = pedido.Cliente
                };

                _pedidoService.Pedidoentregado(pedidoEntregado);

                _pedidoService.EliminarPedido(pedido);
                return NoContent();
            }

            return BadRequest();
        }
    }
}
