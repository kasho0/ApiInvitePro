namespace InviteMasterAPI.Model
{
    public class MesaRegalos
    {
        public int? IdMesaRegalos { get; set; }
        public int? IdEvento { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Url { get; set; }
        public int? Index { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
