using InviteMasterAPI.DataAccess;
using InviteMasterAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace InviteMasterAPI.BusinessLogic
{
    public class Invitado_BL
    {

        /// <summary>
        /// Data access object for Invitado.
        /// </summary>
        Invitado_DA invitadoDA;

        /// <summary>
        /// Initializes a new instance of the <see cref="Invitado_BL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Invitado_BL(IConfiguration configuration)
        {
            invitadoDA = new Invitado_DA(configuration);
        }

        /// <summary>
        /// Inserts a new guest into the database.
        /// </summary>
        /// <param name="invitado">The guest to be inserted.</param>
        public void InsertarInvitado(Invitado invitado)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    int idInvitado = invitadoDA.Insert(invitado);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    // Handle the exception or throw it to be handled at a higher level
                    throw;
                }
            }
        }

        /// <summary>
        /// Retrieves guests from the database based on the invitation ID.
        /// </summary>
        /// <param name="idInvitacion">The ID of the invitation.</param>
        /// <returns>A list of guests associated with the specified invitation ID.</returns>
        public IEnumerable<Invitado> GetByIdInvitacion(int idInvitacion)
        {
            if (idInvitacion == 0)
            {
                throw new ArgumentException("idInvitacion cannot be null or 0.");
            }

            return invitadoDA.Get(new Invitado { IdInvitacion = idInvitacion });
        }

        /// <summary>
        /// Retrieves guests from the database based on the invitation ID.
        /// </summary>
        /// <param name="idInvitacion">The ID of the invitation.</param>
        /// <returns>A list of guests associated with the specified invitation ID.</returns>
        public IEnumerable<Invitado> GetByInvId(string invId)
        {
            if (string.IsNullOrEmpty(invId))
            {
                throw new ArgumentException("invId cannot be null or empty.");
            }

            return invitadoDA.Get(new Invitado { }, invId);
        }

        /// <summary>
        /// Updates an existing guest in the database.
        /// </summary>
        /// <param name="invitado">The guest to be updated.</param>
        /// <exception cref="ArgumentException">Thrown when IdInvitado is null or 0.</exception>
        public void Patch(Invitado invitado)
        {
            if (invitado.IdInvitado == 0)
            {
                throw new ArgumentException("IdInvitado cannot be null or 0.");
            }

            invitadoDA.Patch(invitado);
        }

    }
}
