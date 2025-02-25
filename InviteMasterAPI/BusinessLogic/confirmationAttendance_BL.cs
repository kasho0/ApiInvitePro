using InviteMasterAPI.DataAccess;
using InviteMasterAPI.Infrastructure.Services.Twilio;
using InviteMasterAPI.Interfaces.Infrastructure;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.BusinessLogic
{
    public class confirmationAttendance_BL
    {
        ISendConfirmation _sendConfirmation;

        /// <summary>
        /// Initializes a new instance of the <see cref="confirmationAttendance_BL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public confirmationAttendance_BL(ISendConfirmation sendConfirmation)
        {
            _sendConfirmation = sendConfirmation;
        }

        public string SendConfirmation(ConfirmationAttendance confirmationAttendance)
        {
            confirmationAttendance.Destination = "ernestomolinares@gmail.com";
            var result = _sendConfirmation.Send(confirmationAttendance);

            return result;
        }

    }
}
