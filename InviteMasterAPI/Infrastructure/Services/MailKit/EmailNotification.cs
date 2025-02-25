using InviteMasterAPI.Interfaces.Infrastructure;
using InviteMasterAPI.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace InviteMasterAPI.Infrastructure.Services.MailKit
{
    public class EmailNotification : ISendConfirmation
    {
        private readonly SmtpOptions _smtpOptions;

        public EmailNotification(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public string Send(ConfirmationAttendance confirmationAttendance)
        {
            var toEmail = confirmationAttendance.Destination;
            var subject = "Confirmación de Asistencia";
            var body = $"Confirmación de Asistencia:\n\nNombre: {confirmationAttendance.Name}\nAsistencia: {(confirmationAttendance.Attendance ? "Sí" : "No")}\nAdultos: {confirmationAttendance.NumberAdults}\nNiños: {confirmationAttendance.NumberChildren}\nComentarios: {confirmationAttendance.Comments}";

            SendEmailAsync(toEmail, subject, body).Wait();
            return "Correo enviado con éxito";
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        //private readonly SmtpOptions _smtpOptions;
        //IConfiguration _configuration;

        //public EmailNotification(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public string Send(ConfirmationAttendance confirmationAttendance)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task SendEmailAsync(string toEmail, string subject, string body)
        //{

        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail));
        //    message.To.Add(new MailboxAddress("", toEmail));
        //    message.Subject = subject;
        //    message.Body = new TextPart("plain") { Text = body };

        //    using var client = new SmtpClient();
        //    await client.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //    await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
        //    await client.SendAsync(message);
        //    await client.DisconnectAsync(true);
        //}


    }
}
