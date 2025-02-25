using System;

namespace InviteMasterAPI.Model
{    
    /// <summary>
    /// Clase que representa la tabla Boda
    /// </summary>
    public class Boda
    {
        public int? IdBoda { get; set; }
        public int? IdEvento { get; set; }
        public bool? Civil { get; set; }
        public string? CivilLugar { get; set; }
        public string? CivilDireccion { get; set; }
        public string? CivilUbicacion { get; set; }
        public string? CivilHorario { get; set; }
        public bool? Iglesia { get; set; }
        public string? IglesiaLugar { get; set; }
        public string? IglesiaDireccion { get; set; }
        public string? IglesiaUbicacion { get; set; }
        public string? IglesiaHorario { get; set; }
        public bool? Local { get; set; }
        public string? LocalLugar { get; set; }
        public string? LocalDireccion { get; set; }
        public string? LocalUbicacion { get; set; }
        public string? LocalHorario { get; set; }
        public bool? NoNinos { get; set; }
        public string? Padrinos { get; set; }
        public bool? SobreRegalo { get; set; }
        public string? SobreRegaloTexto { get; set; }
        public bool? Transferencia { get; set; }
        public string? TransferenciaTexto { get; set; }
        public string? TransferenciaDatos { get; set; }
        public bool? MesaRegalos { get; set; }
        public string? MesaRegalosTexto { get; set; }
        public Catalogos.CatEtiqueta? CatEtiqueta { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }

    }
}