﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UbicacionesController : ControllerBase
    {
        private readonly IUbicacionService _ubicacionesService;

        public UbicacionesController(IUbicacionService ubicacionesService)
        {
            _ubicacionesService = ubicacionesService;
        }

        // GET: api/Ubicaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubicacion>>> GetUbicaciones()
        {
            //Obtiene las ubicaciones
            var ubicaciones = await _ubicacionesService.ObtenerUbicaciones();
            return Ok(ubicaciones);
        }

        // GET: api/Ubicaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ubicacion>> GetUbicacion(int id)
        {
            //Obtiene la ubicacion por Id
            var ubicacion = await _ubicacionesService.ObtenerUbicacion(id);

            if (ubicacion == null)
            {
                return NotFound();
            }

            return ubicacion;
        }

        // PUT: api/Ubicaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUbicacion(int id, Ubicacion ubicacion)
        {
            //Actualiza la ubicacion en concreto
            var resultado = await _ubicacionesService.ActualizarUbicacion(id, ubicacion);

            if (resultado)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Ubicaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public async Task<ActionResult<UbicacionDTO>> PostUbicacion(UbicacionDTO ubicacion)
        {
            //Agraga una nueva ubicacion
            UbicacionDTO nuevaUbicacion = await _ubicacionesService.AgregarUbicacion(ubicacion);

            return CreatedAtAction("GetUbicacion", new { id = nuevaUbicacion.UbicacionId }, nuevaUbicacion);
        }

        // DELETE: api/Ubicaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUbicacion(int id)
        {
            //Borra la ubicacion
            var resultado = await _ubicacionesService.EliminarUbicacion(id);

            if (resultado)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("pedidos")]
        public async Task<ActionResult<Ubicacion>> ObtenerUbicacionMasReciente(int pedidoId)
        {

            //En este caso lo que hace es coger del registro de cada ubicacion a través del pedido su ubicación más reciente en el reparto, así en el front se puede calcular el tiempo restante.
            var ubicacion = await _ubicacionesService.ObtenerUbicacionMasReciente(pedidoId);
            if (ubicacion == null)
            {
                return NotFound();
            }

            return ubicacion;
        }
    }
}
