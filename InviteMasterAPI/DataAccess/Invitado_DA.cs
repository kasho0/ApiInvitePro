using System.Data;
using System.Data.SqlClient;
using Dapper;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.DataAccess
{
    public class Invitado_DA : BaseDataAccess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Invitado_DA"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Invitado_DA(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Inserts a new guest into the database.
        /// </summary>
        /// <param name="invitado">The guest to be inserted.</param>
        /// <returns>The ID of the inserted guest.</returns>
        public int Insert(Invitado invitado)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdInvitacion", invitado.IdInvitacion);
                parameters.Add("@Nombre", invitado.Nombre);
                parameters.Add("@Email", invitado.Email);
                parameters.Add("@CelularNumero", invitado.CelularNumero);

                // Ejecutar el procedimiento almacenado y capturar el ID insertado
                int idInvitado = db.QuerySingle<int>(
                    "sp_Invitado_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return idInvitado;
            }
        }

        /// <summary>
        /// Retrieves guests from the database based on the specified filters.
        /// </summary>
        /// <param name="idInvitado">The ID of the guest to filter by (optional).</param>
        /// <param name="idInvitacion">The ID of the invitation to filter by (optional).</param>
        /// <param name="nombre">The name of the guest to filter by (optional).</param>
        /// <param name="email">The email of the guest to filter by (optional).</param>
        /// <param name="celularNumero">The phone number of the guest to filter by (optional).</param>
        /// <param name="catInvitadoStatus">The status of the guest to filter by (optional).</param>
        /// <returns>A list of guests that match the specified filters.</returns>
        public IEnumerable<Invitado> Get(Invitado invitado, string invId = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdInvitado", invitado.IdInvitado);
                parameters.Add("@IdInvitacion", invitado.IdInvitacion);
                parameters.Add("@Nombre", invitado.Nombre);
                parameters.Add("@Email", invitado.Email);
                parameters.Add("@CelularNumero", invitado.CelularNumero);
                parameters.Add("@CodigoCatInvitadoStatus", invitado.CodigoCatInvitadoStatus);
                parameters.Add("@InvId", invId);

                return db.Query<Invitado>("sp_Invitado_Get", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Updates an existing guest in the database.
        /// </summary>
        /// <param name="idInvitado">The ID of the guest to be updated.</param>
        /// <param name="nombre">The name of the guest (optional).</param>
        /// <param name="email">The email of the guest (optional).</param>
        /// <param name="celularNumero">The phone number of the guest (optional).</param>
        /// <param name="catInvitadoStatus">The status category of the guest (optional).</param>
        public void Patch(Invitado invitado)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdInvitado", invitado.IdInvitado);
                parameters.Add("@Nombre", invitado.Nombre);
                parameters.Add("@Email", invitado.Email);
                parameters.Add("@CelularNumero", invitado.CelularNumero);
                parameters.Add("@CodigoCatInvitadoStatus", invitado.CodigoCatInvitadoStatus);

                db.Execute("sp_Invitado_Patch", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
