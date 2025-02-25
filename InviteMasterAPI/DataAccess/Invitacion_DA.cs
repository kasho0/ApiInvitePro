using System.Data;
using System.Data.SqlClient;
using Dapper;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.DataAccess
{
    public class Invitacion_DA : BaseDataAccess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Invitacion_DA"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Invitacion_DA(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Inserts a new invitation into the database.
        /// </summary>
        /// <param name="invitacion">The invitation to be inserted.</param>
        /// <returns>The ID of the inserted invitation.</returns>
        public int Insert(Invitacion invitacion)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdEvento", invitacion.IdEvento);
                parameters.Add("@NoNinos", invitacion.NoNinos);
                parameters.Add("@PorParteDe", invitacion.PorParteDe);

                // Ejecutar el procedimiento almacenado y capturar el ID insertado
                int idInvitacion = db.QuerySingle<int>(
                    "sp_Invitacion_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return idInvitacion;
            }
        }

        /// <summary>
        /// Retrieves invitations from the database based on the specified filters.
        /// </summary>
        /// <param name="invitacion"></param>
        /// <returns>A list of invitations that match the specified filters.</returns>
        public IEnumerable<Invitacion> Get(Invitacion invitacion)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdInvitacion", invitacion.IdInvitacion);
                parameters.Add("@IdEvento", invitacion.IdEvento);
                parameters.Add("@NoNinos", invitacion.NoNinos);
                parameters.Add("@Activo", invitacion.Activo);
                parameters.Add("@InvId", invitacion.InvId);

                return db.Query<Invitacion>("sp_Invitacion_Get", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Updates an existing invitation in the database.
        /// </summary>
        /// <param name="invitacion"></param>
        public void Patch(Invitacion invitacion)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdInvitacion", invitacion.IdInvitacion);
                parameters.Add("@IdEvento", invitacion.IdEvento);
                parameters.Add("@NoNinos", invitacion.NoNinos);
                parameters.Add("@Activo", invitacion.Activo);

                db.Execute("sp_Invitacion_Patch", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
