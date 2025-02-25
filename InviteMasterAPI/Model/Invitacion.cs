namespace InviteMasterAPI.Model
{
    public class Invitacion
    {
        public int? IdInvitacion { get; set; }
        public string? InvId { get; set; }
        public int? IdEvento { get; set; }
        public bool? NoNinos { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
        public string? QrCode { get; set; }
        public string? PorParteDe {  get; set; }

        public List<Invitado>? Invitados { get; set; }
    }
}
