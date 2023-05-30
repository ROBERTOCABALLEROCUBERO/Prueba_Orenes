using Microsoft.AspNetCore.Mvc;
using Orenes.Models;
using Orenes.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosEntregadosController : ControllerBase
    {
        private readonly IPedidosEntregadosService _pedidoEntregadosService;

        public PedidosEntregadosController(IPedidosEntregadosService pedidoEntregadosService)
        {
            _pedidoEntregadosService = pedidoEntregadosService;
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

    }
}
