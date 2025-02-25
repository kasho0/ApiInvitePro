namespace InviteMasterAPI.Model
{
    public class ConfirmationAttendance
    {
        public bool Attendance { get; set; }
        public string Name { get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public string Comments { get; set; }
        public string? Destination { get; set; }
    }
}
