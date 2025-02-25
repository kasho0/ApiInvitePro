using InviteMasterAPI.DataAccess;
using InviteMasterAPI.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace InviteMasterAPI.BusinessLogic
{
    public class Evento_BL
    {

        /// <summary>
        /// Configuration settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Evento_BL"/> class.
        /// </summary>
        Evento_DA evento_DA;

        /// <summary>
        /// Initializes a new instance of the <see cref="Evento_BL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Evento_BL(IConfiguration configuration)
        {
            _configuration = configuration;
            evento_DA = new Evento_DA(configuration);
        }

        /// <summary>
        /// Inserts a new event into the database.
        /// </summary>
        /// <param name="evento">The event to be inserted.</param>
        /// <exception cref="ArgumentNullException">Thrown when the event of type BODA does not have Boda information.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the transaction.</exception>
        public void Insert(Evento evento)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    // validate evento.CatEventoTipo is not null
                    if (evento.CatEventoTipo == null)
                    {
                        throw new ArgumentNullException(nameof(evento.CatEventoTipo), "El tipo de evento no puede ser nulo.");
                    }

                    int idEvento = evento_DA.Insert(evento);

                    // Aquí se implementa la lógica para insertar el evento en la base de datos
                    // Llamar a la capa de acceso a datos para realizar la inserción
                    if (evento.CatEventoTipo == Catalogos.CatEventoTipo.BODA)
                    {
                        // Validar que evento.Boda no sea nulo
                        if (evento.Boda == null)
                        {
                            throw new ArgumentNullException(nameof(evento.Boda), "El evento de tipo BODA debe tener información de Boda.");
                        }

                        // Lógica específica para eventos de tipo boda
                        evento.Boda.IdEvento = idEvento;
                        var bodaDA = new Boda_DA(_configuration);
                        bodaDA.Insert(evento.Boda);
                    }
                    else if (evento.CatEventoTipo == Catalogos.CatEventoTipo.BABYSHOWER)
                    {
                        // Lógica específica para eventos de tipo cumpleaños
                    }
                    else
                    {
                        // Lógica para otros tipos de eventos
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Manejar la excepción o lanzarla para que sea manejada en un nivel superior
                    throw;
                }
            }
        }

        /// <summary>
        /// Retrieves all events from the database.
        /// </summary>
        /// <returns>A collection of all events.</returns>
        public IEnumerable<Evento> Get()
        {
            return evento_DA.Get(new Evento());
        }

        /// <summary>
        /// Retrieves an event from the database based on the specified ID.
        /// </summary>
        /// <param name="idEvento">The ID of the event to retrieve.</param>
        /// <returns>The event with the specified ID, including related data.</returns>
        public Evento GetById(int idEvento)
        {
            if (idEvento == 0)
            {
                throw new ArgumentException("IdEvent cannot be null or 0.");
            }

            var evento = evento_DA.Get(new Evento { IdEvento = idEvento }).FirstOrDefault();

            if (evento != null)
            {
                // Validar el tipo de evento y buscar el objeto correspondiente
                switch (evento.CatEventoTipo)
                {
                    case Catalogos.CatEventoTipo.BODA:
                        evento.Boda = GetBodaByIdEvento(idEvento);
                        break;
                        // Agregar lógica para otros tipos de eventos en el futuro
                }

                // Buscar las propiedades adicionales
                var mesaRegalosBL = new MesaRegalos_BL(_configuration);
                evento.MesasRegalos = mesaRegalosBL.GetByIdEvent(idEvento).ToList();

                var invitacionBL = new Invitacion_BL(_configuration);
                evento.Invitaciones = invitacionBL.GetByIdEvent(idEvento).ToList();

                //TODO: Pendiente
                //var galeriaBL = new Galeria_BL(_configuration);
                //evento.Galeria = galeriaBL.GetByEventoId(idEvento);
            }

            return evento;
        }

        /// <summary>
        /// Retrieves the wedding information associated with the specified event ID.
        /// </summary>
        /// <param name="idEvento">The ID of the event.</param>
        /// <returns>The wedding information associated with the specified event ID, or null if not found.</returns>
        private Boda? GetBodaByIdEvento(int idEvento)
        {
            var bodaDA = new Boda_DA(_configuration);
            return bodaDA.Get(new Boda { IdEvento = idEvento }).FirstOrDefault();
        }

        /// <summary>
        /// Updates an existing event in the database using the Patch method.
        /// </summary>
        /// <param name="idEvento">The ID of the event to be updated.</param>
        /// <param name="nombreEvento">The new name of the event (optional).</param>
        /// <param name="fechaEvento">The new date of the event (optional).</param>
        /// <param name="fotoPrincipalUrl">The new URL of the main photo of the event (optional).</param>
        /// <param name="catEventoStatus">The new status of the event (optional).</param>
        public void Patch(Evento evento)
        {
            evento_DA.Patch(evento);

            switch (evento.CatEventoTipo)
            {
                case Catalogos.CatEventoTipo.BODA:
                    evento.Boda.IdEvento = evento.IdEvento;
                    PatchBoda(evento.Boda);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates an existing wedding event in the database.
        /// </summary>
        /// <param name="boda"></param>
        public void PatchBoda(Boda boda)
        {
            var bodaDA = new Boda_DA(_configuration);
            bodaDA.Patch(boda);
        }

    }
}
