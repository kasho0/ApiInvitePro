namespace InviteMasterAPI.Model.Response
{
    public class Invitation
    {
        public int? idInvitacion {  get; set; }
        public bool? ninos {  get; set; }
        public bool? activo { get; set; }
        public int? numeroInvitados { get; set; }
        public string? porParteDe {  get; set; }
        public string? invId {  get; set; }
        public string? nombresInvitados { get; set; }
        public IEnumerable<Response.Invitado>? invitados { get; set; }
    }
}
