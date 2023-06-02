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

       
     

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> ObtenerPedidosGeneral()
        {
            var pedidos = await _pedidoService.ObtenerPedidos();
            return Ok(pedidos);
        }

        [HttpGet("{pedidoId}")]
        public async Task<ActionResult<Pedido>> ObtenerPedido(int pedidoId)
        {
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);
            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }
        [Authorize]
        [HttpGet("PedidosCliente")]
        public async Task<ActionResult<List<PedidoDTO>>> PedidosCliente()
        {
            string usuario = User.FindFirst(ClaimTypes.Name).Value;

            var cliente = await _clienteService.ObtenerDatosUsuarioPorNombre(usuario);
            var pedidos = await _pedidoService.ObtenerPedidosPorIdUsuario(cliente.ClienteId);

            return Ok(pedidos);
        }

        [HttpGet("PedidosVehiculo")]
        public async Task<ActionResult<List<PedidoDTO>>> PedidosVehiculo(int vehiculoId)
        {

    
            var pedidos = await _pedidoService.ObtenerPedidosPorVehiculo(vehiculoId);

            return Ok(pedidos);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Pedido>> CrearPedido(PedidoDTO pedido)
        {
            string usuario = User.FindFirst(ClaimTypes.Name).Value;
            var cliente = await _clienteService.ObtenerDatosUsuarioPorNombre(usuario);
            var pedidoId = await _pedidoService.CrearPedido(pedido, cliente);
            return CreatedAtAction(nameof(ObtenerPedido), new { pedidoId }, pedido);
        }

        [HttpPost("MarcarEnProceso")]
        public async Task<IActionResult> MarcarPedidoEnProceso(int pedidoId, int vehiculoId)
        {
            // Obtener el pedido original de la base de datos
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);

            if (pedido != null)
            {
                // Actualizar el estado del pedido a "EnProceso"
                pedido.status = EstadoPedido.EnProceso;
                pedido.VehiculoId1 = vehiculoId;

                // Actualizar el pedido en el servicio
                await _pedidoService.ActualizarPedido(pedido);

                return Ok(); // O cualquier otro resultado que desees devolver en caso de éxito
            }

            return NotFound(); // O cualquier otro resultado que desees devolver en caso de no encontrar el pedido
        }


        [HttpPut("{pedidoId}")]
        public async Task<IActionResult> ActualizarPedido(int pedidoId, Pedido pedido)
        {
            if (pedidoId != pedido.PedidoId)
                return BadRequest();

            var resultado = await _pedidoService.ActualizarPedido(pedido);
            if (!resultado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{pedidoId}")]
        public async Task<IActionResult> EliminarPedido(int pedidoId)
        {
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);
            var resultado = await _pedidoService.EliminarPedido(pedido);
            if (!resultado)
            {
                return NotFound();
            }
            return NoContent();
        }


        [HttpPost("MarcarEntregado")]
        public async Task<IActionResult> MarcarPedidoComoEntregado(int pedidoId)
        {
            // Obtener el pedido original de la base de datos
            var pedido = await _pedidoService.ObtenerPedido(pedidoId);

            if (pedido != null)
            {

                // Crear una instancia de PedidoEntregado y copiar los datos relevantes
                var pedidoEntregado = new PedidoEntregado
                {
                    ClienteId = pedido.ClienteId,
                    DireccionEntrega = pedido.DireccionEntrega,
                    Status = EstadoPedido.Entregado,
                    Cliente = pedido.Cliente
                };

                // Guardar el pedido entregado en la tabla correspondiente
                _pedidoService.Pedidoentregado(pedidoEntregado);

                // Eliminar el pedido original de la tabla de pedidos
                _pedidoService.EliminarPedido(pedido);
                return NoContent(); 
            }

            return BadRequest();
        }

    }
}
