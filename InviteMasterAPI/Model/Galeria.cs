namespace InviteMasterAPI.Model
{
    public class Galeria
    {
        public int? IdGaleria { get; set; }
        public int? IdEvento { get; set; }
        public int? orden { get; set; }
        public string? Descripcion { get; set; }
        public string? Url { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
