using InviteMasterAPI.BusinessLogic;
using InviteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML.Messaging;

namespace InviteMasterAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class confirmationAttendanceController : Controller
    {
        private readonly IConfiguration _configuration;
        confirmationAttendance_BL _confirmationAttendance_BL;

        public confirmationAttendanceController(IConfiguration configuration, confirmationAttendance_BL confirmationAttendance)
        {
            _configuration = configuration;
            _confirmationAttendance_BL = confirmationAttendance;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ConfirmationAttendance confirmationAttendance)
        {
            var result = _confirmationAttendance_BL.SendConfirmation(confirmationAttendance);

            return Ok(new { result });
        }
    }
}
