using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using InviteMasterAPI.Model;
using System.Collections.Generic;
using System.Linq;
using InviteMasterAPI.BusinessLogic;

namespace InviteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly Evento_BL _eventoBL;

        public EventController(IConfiguration configuration)
        {
            _eventoBL = new Evento_BL(configuration);
        }        

        // POST api/event
        [HttpPost]
        public IActionResult Post([FromBody] Evento evento)
        {
            if (evento == null)
            {
                return BadRequest("Evento is null.");
            }
            _eventoBL.Insert(evento);
            return CreatedAtAction(nameof(Post), new { id = evento.IdEvento }, evento);
        }

        // GET api/event
        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            var eventos = _eventoBL.Get();
            return Ok(eventos);
        }

        // GET api/event/{id}
        [HttpGet("{id}")]
        public ActionResult<Evento> GetById(int id)
        {
            var evento = _eventoBL.GetById(id);
            if (evento == null)
            {
                return NotFound();
            }
            return Ok(evento);
        }

        // GET api/event/search
        [HttpGet("search")]
        public ActionResult<IEnumerable<Evento>> GetByParameter(
            [FromQuery] string? nombreEvento,
            [FromQuery] DateTime? fechaEvento,
            [FromQuery] string? catEventoTipo,
            [FromQuery] DateTime? fechaCreacion,
            [FromQuery] DateTime? fechaModificacion,
            [FromQuery] string? catEtiqueta)
        {
            //var eventos = _eventoBL.Get

            //if (eventosEncontrados == null || !eventosEncontrados.Any())
            //{
            //    return NotFound();
            //}
            //return Ok(eventosEncontrados);

            return NotFound();
        }

        // PATCH api/event/{id}
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Evento evento)
        {
            if (evento == null)
            {
                return BadRequest("Evento is null.");
            }
            if(id == 0)
            {
                return BadRequest("Id cannot be null or 0.");
            }

            try
            {
                evento.IdEvento = id;
                _eventoBL.Patch(evento);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            return Ok();
        }


    }
}
