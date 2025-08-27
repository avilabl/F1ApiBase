using Microsoft.AspNetCore.Mvc;
using F1ApiBase.Models;
using F1ApiBase.Services.Interfaces;

namespace F1ApiBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PilotosController : ControllerBase
    {
        private readonly IPilotoService _pilotoService;

        public PilotosController(IPilotoService pilotoService)
        {
            _pilotoService = pilotoService;
        }

        // GET: get all para traer todos los objetos que haya (api/Pilotos)
        [HttpGet]
        public IActionResult GetAll()
        {
            var pilotos = _pilotoService.GetAllPilotos();
            return Ok(pilotos);
        }

        // GET: get para conseguir un objeto particular por id (api/Pilotos/{id})
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var piloto = _pilotoService.GetPilotoById(id);
            if (piloto == null)
                return NotFound();
            return Ok(piloto);
        }

        // POST: post para agregar un objeto (api/Pilotos)
        [HttpPost]
        public IActionResult Create([FromBody] Piloto piloto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creado = _pilotoService.AddPiloto(piloto);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        // PUT: put para actualizar un objeto (api/Pilotos/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Piloto piloto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            piloto.Id = id;
            var actualizado = _pilotoService.UpdatePiloto(piloto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: delete para borrar un objeto (api/Pilotos/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var eliminado = _pilotoService.DeletePiloto(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}