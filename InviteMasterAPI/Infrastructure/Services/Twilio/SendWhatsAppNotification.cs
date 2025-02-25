using InviteMasterAPI.Model;
using Twilio.Types;
using Twilio;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using InviteMasterAPI.Interfaces.Infrastructure;

namespace InviteMasterAPI.Infrastructure.Services.Twilio
{
    public class SendWhatsAppNotification : ISendConfirmation
    {
        IConfiguration _configuration;

        public SendWhatsAppNotification(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Send(ConfirmationAttendance confirmationAttendance)
        {
            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];
            var fromNumber = _configuration["Twilio:FromNumber"];
            var toNumber = "whatsapp:+5216622969648";

            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
            body: $"Confirmación de Asistencia:\nNombre: {confirmationAttendance.Name}\nAsistencia: {(confirmationAttendance.Attendance ? "Sí" : "No")}\nAdultos: {confirmationAttendance.NumberAdults}\nNiños: {confirmationAttendance.NumberChildren}\nComentarios: {confirmationAttendance.Comments}",
            from: new PhoneNumber(fromNumber),
            to: new PhoneNumber(toNumber)
            );

            return message.Sid;
        }
    }
}
