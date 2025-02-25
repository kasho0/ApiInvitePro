using InviteMasterAPI.DataAccess;
using InviteMasterAPI.Model;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace InviteMasterAPI.BusinessLogic
{
    public class Invitacion_BL
    {

        /// <summary>
        /// Configuration settings.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Data access object for invitations.
        /// </summary>
        Invitacion_DA invitacionDA;

        Invitado_DA invitado_DA;

        /// <summary>
        /// Initializes a new instance of the <see cref="Invitacion_BL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Invitacion_BL(IConfiguration configuration)
        {
            _configuration = configuration;
            invitacionDA = new Invitacion_DA(configuration);
            invitado_DA = new Invitado_DA(configuration);
        }

        /// <summary>
        /// Inserts a new invitation into the database.
        /// </summary>
        /// <param name="invitacion">The invitation to be inserted.</param>
        public void Insert(Invitacion invitacion)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    int idInvitacion = invitacionDA.Insert(invitacion);

                    foreach (var invitado in invitacion.Invitados)
                    {
                        invitado.IdInvitacion = idInvitacion;
                        invitado_DA.Insert(invitado);
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Handle the exception or rethrow it to be handled at a higher level
                    throw;
                }
            }
        }

        /// <summary>
        /// Retrieves invitations by event ID.
        /// </summary>
        /// <param name="idEvento">The ID of the event.</param>
        /// <returns>A list of invitations for the specified event.</returns>
        public IEnumerable<Invitacion> GetByIdEvent(int idEvento)
        {
            var invitaciones = invitacionDA.Get(new Invitacion { IdEvento = idEvento });

            Invitado_BL invitadoBL = new Invitado_BL(_configuration);
            foreach (var invitacion in invitaciones)
            {
                invitacion.Invitados = invitadoBL.GetByIdInvitacion((int)invitacion.IdInvitacion).ToList();
            }

            return invitaciones;
        }

        /// <summary>
        /// Updates an existing invitation in the database using the Patch method.
        /// </summary>
        /// <param name="invitacion"></param>
        public void Patch(Invitacion invitacion)
        {
            invitacionDA.Patch(invitacion);
        }

    }
}
