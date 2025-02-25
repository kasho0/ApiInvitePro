using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using InviteMasterAPI.Model;
using System.Collections.Generic;
using System.Linq;
using InviteMasterAPI.BusinessLogic;
using System;

namespace InviteMasterAPI.Controllers
{
    public class GuestController : ControllerBase
    {

        private readonly Invitado_BL _invitadoBL;
        private readonly Invitacion_BL _invitacionBl;

        public GuestController(IConfiguration configuration)
        {
            _invitadoBL = new Invitado_BL(configuration);
            _invitacionBl = new Invitacion_BL(configuration);
        }

        // Metodo get para obtener los invitados de un evento. recibe como parámetro el invId.
        [HttpGet("api/guest/{invId}")]
        public ActionResult<IEnumerable<Invitado>> Get(Guid invId)
        {
            var invitados = _invitadoBL.GetByInvId(invId.ToString());
            
            if (invitados == null || invitados.Count() < 1) {
                return BadRequest();
            }

            return Ok(invitados);
        }

        // Metodo para confirmar la asistencia de un invitado. recibe como parámetro el id del invitado, el id del evento y el estado de la confirmación.
        [HttpPatch("api/guest/confirm")]    
        public IActionResult Confirm([FromBody] Invitado invitado)
        {
            if (invitado == null)
            {
                return BadRequest("Patch is null.");
            }
            _invitadoBL.Patch(invitado);
            return Ok();
        }

        // metodo para guardar invitaciones con invitados
        [HttpPost("api/invitation")]
        public IActionResult SaveInvitation([FromBody] Invitacion invitacion)
        {
            if (invitacion == null && invitacion.Invitados?.Count > 0)
            {
                return BadRequest();
            }
            _invitacionBl.Insert(invitacion);
            return Ok();
        }

        [HttpPatch("api/invitation/{idInvitacion}/delete")]
        public IActionResult DeleteInvitation(int idInvitacion)
        {
            if (idInvitacion < 1)
            {
                return BadRequest("Patch is null.");
            }
            _invitacionBl.Patch(new Invitacion
            {
                IdInvitacion = idInvitacion,
                Activo = false
            });

            var invitadosList = _invitadoBL.GetByIdInvitacion(idInvitacion);

            foreach (var invItem in invitadosList)
            {
                _invitadoBL.Patch(new Invitado
                {
                    IdInvitado = invItem.IdInvitado,
                    CodigoCatInvitadoStatus = "C"
                });
            }

            return Ok();
        }

        [HttpGet("api/invitation/{idEvent}")]
        public ActionResult<IEnumerable<Invitacion>> GetInvitacions(int idEvent)
        {
            var invitaciones = _invitacionBl.GetByIdEvent(idEvent);

            List<Model.Response.Invitation> response = new List<Model.Response.Invitation>();

            foreach (var itemInvitacion in invitaciones)
            {
                response.Add(new Model.Response.Invitation
                {
                    idInvitacion = itemInvitacion.IdInvitacion,
                    ninos = !itemInvitacion.NoNinos,
                    activo = itemInvitacion.Activo,
                    numeroInvitados = itemInvitacion.Invitados.Count,
                    porParteDe = itemInvitacion.PorParteDe,
                    invId = itemInvitacion.InvId,
                    nombresInvitados = GetNombresInvitados(itemInvitacion.Invitados),
                    invitados = CreateObjInvitados(itemInvitacion.Invitados)
                });
            }


            return Ok(response);
        }

        private IEnumerable<Model.Response.Invitado>? CreateObjInvitados(List<Invitado> invitados)
        {
            List<Model.Response.Invitado> objInvList = new List<Model.Response.Invitado>();

            foreach (var item in invitados)
            {
                objInvList.Add(new Model.Response.Invitado
                {
                    idInvitado = item.IdInvitado,
                    nombre = item.Nombre,
                    email = item.Email,
                    numeroCelular = item.CelularNumero,
                    estatus = item.CodigoCatInvitadoStatus
                });
            }

            return objInvList;
        }

        private string GetNombresInvitados(IEnumerable<Invitado> invitados)
        {
            return string.Join(", ", invitados.Select(i => i.Nombre));
        }

    }
}
