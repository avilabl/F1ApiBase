using F1ApiBase.Models;
using F1ApiBase.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace F1ApiBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CircuitosController : ControllerBase
    {
        private readonly ICircuitoService _circuitoService;

        public CircuitosController(ICircuitoService circuitoService)
        {
            _circuitoService = circuitoService;
        }

        // GET: get all para traer todos los objetos que haya(api/Circuitos)
        [HttpGet]
        public IActionResult GetAll()
        {
            var circuitos = _circuitoService.GetAllCircuitos();
            return Ok(circuitos);
        }

        // GET: get para conseguir un objeto particular por id (api/Circuitos/{id})
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var circuito = _circuitoService.GetCircuitoById(id);
            if (circuito == null)
                return NotFound();
            return Ok(circuito);
        }

        // POST: post para agregar un objeto (api/Circuitos)
        [HttpPost]
        public IActionResult Create([FromBody] Circuito circuito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = _circuitoService.AddCircuito(circuito);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        // PUT: put para actualizar un objeto (api/Circuitos/{id})
        [HttpPut("{id}")]
        public IActionResult UpdateCircuito(int id, [FromBody] Circuito circuito)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = _circuitoService.GetCircuitoById(id);
            if (existente == null)
                return NotFound();

            circuito.Id = id;
            var actualizado = _circuitoService.UpdateCircuito(circuito);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: delete para borrar un objeto (api/Circuitos/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existente = _circuitoService.DeleteCircuito(id);
            if (!existente)
                return NotFound();

            _circuitoService.DeleteCircuito(id);
            return NoContent();
        }
    }
}