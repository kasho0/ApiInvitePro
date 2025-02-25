namespace InviteMasterAPI.Model
{
    public class Invitado
    {
        public int? IdInvitado { get; set; }
        public int? IdInvitacion { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? CelularNumero { get; set; }
        public string? CodigoCatInvitadoStatus { get; set; }
    }
}
