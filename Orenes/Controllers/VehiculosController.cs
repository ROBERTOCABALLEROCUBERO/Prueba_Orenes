using Microsoft.AspNetCore.Mvc;
using Orenes.Models;
using Microsoft.AspNetCore.Mvc;
using Orenes.Models;
using Orenes.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculosService _vehiculosService;

        public VehiculosController(IVehiculosService vehiculosService)
        {
            _vehiculosService = vehiculosService;
        }

        // GET: api/Vehiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
            var vehiculos = await _vehiculosService.ObtenerVehiculos();
            return Ok(vehiculos);
        }

        // GET: api/Vehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var vehiculo = await _vehiculosService.ObtenerVehiculo(id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return vehiculo;
        }

        // PUT: api/Vehiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            var resultado = await _vehiculosService.ActualizarVehiculo(id, vehiculo);

            if (resultado)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Vehiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
            var nuevoVehiculo = await _vehiculosService.AgregarVehiculo(vehiculo);
            return CreatedAtAction(nameof(GetVehiculo), new { id = nuevoVehiculo.VehiculoId }, nuevoVehiculo);
        }

        // DELETE: api/Vehiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var resultado = await _vehiculosService.EliminarVehiculo(id);

            if (resultado)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

