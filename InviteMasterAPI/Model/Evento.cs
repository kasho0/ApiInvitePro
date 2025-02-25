using System;

namespace InviteMasterAPI.Model
{
    public class Evento
    {
        public int? IdEvento { get; set; }
        public string? NombreEvento { get; set; }
        public DateTime? FechaEvento { get; set; }
        public Catalogos.CatEventoTipo? CatEventoTipo { get; set; }
        public string? FotoPrincipalUrl { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public Catalogos.CatEventoStatus? CatEventoStatus { get; set; }

        public Boda? Boda { get; set; }
        public List<MesaRegalos>? MesasRegalos { get; set; }
        public List<Invitacion>? Invitaciones { get; set; }
        public List<Galeria>? Galeria { get; set; }
    }
}
