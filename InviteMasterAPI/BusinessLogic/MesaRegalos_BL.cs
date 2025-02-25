using InviteMasterAPI.DataAccess;
using InviteMasterAPI.Model;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace InviteMasterAPI.BusinessLogic
{
    public class MesaRegalos_BL
    {
        /// <summary>
        /// Data access object for MesaRegalos.
        /// </summary>
        MesaRegalos_DA mesaRegalosDA;
        /// <summary>
        /// Initializes a new instance of the <see cref="MesaRegalos_BL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public MesaRegalos_BL(IConfiguration configuration)
        {
            mesaRegalosDA = new MesaRegalos_DA(configuration);
        }

        /// <summary>
        /// Inserts a new gift table into the database.
        /// </summary>
        /// <param name="mesaRegalos">The gift table to be inserted.</param>
        public void InsertarMesaRegalos(MesaRegalos mesaRegalos)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    int idMesaRegalos = mesaRegalosDA.Insert(mesaRegalos);

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
        /// Retrieves gift tables by event ID.
        /// </summary>
        /// <param name="idEvent">The ID of the event.</param>
        /// <returns>A list of gift tables for the specified event.</returns>
        public IEnumerable<MesaRegalos> GetByIdEvent(int idEvent)
        {
            if (idEvent == 0)
            {
                throw new ArgumentException("IdEvent cannot be null or 0.");
            }

            return mesaRegalosDA.Get(new MesaRegalos { IdEvento = idEvent });
        }

        /// <summary>
        /// Updates an existing guest in the database using the Patch method.
        /// </summary>
        /// <param name="mesaRegalos"></param>
        public void Patch(MesaRegalos mesaRegalos)
        {
            mesaRegalosDA.Patch(mesaRegalos);
        }
    }
}
