using InviteMasterAPI.Model;

namespace InviteMasterAPI.Interfaces.Infrastructure
{
    public interface ISendConfirmation
    {
        public string Send(ConfirmationAttendance confirmationAttendance);
    }
}
