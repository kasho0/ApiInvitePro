namespace InviteMasterAPI.Model
{
    //TODO: poner en el config
    public static class Catalogos
    {
        public enum CatEtiqueta
        {
            FORMAL,
            BODAPLAYA
        }

        public enum CatEventoStatus
        {
            CREADO,
            ENVIADO,
            FINALIZADO,
            POSPUESTO,
            CANCELADO
        }

        public enum CatEventoTipo
        {
            BODA,
            PINATA,
            BAUTIZO,
            BABYSHOWER,
            XVANOS,
            PRIMERA_COMUNION,
            CUMPLEANOS,
            GRADUACION,
            DESPEDIDA_SOLTERA,
            OTRO
        }

        public enum CatInvitadoStatus
        {
            INVITADO,
            CANCELADO,
            CONFIRMSI,
            CONFIRMNO,
            CONFIRMPEN,
            LLEGO,
            SALIO,
            REINGRESO
        }
    }
}
