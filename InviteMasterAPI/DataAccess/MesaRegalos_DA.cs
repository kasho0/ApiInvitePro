using System.Data;
using System.Data.SqlClient;
using Dapper;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.DataAccess
{
    public class MesaRegalos_DA : BaseDataAccess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MesaRegalos_DA"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public MesaRegalos_DA(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Inserts a new gift table into the database.
        /// </summary>
        /// <param name="mesaRegalos">The gift table to be inserted.</param>
        /// <returns>The ID of the inserted gift table.</returns>
        public int Insert(MesaRegalos mesaRegalos)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdEvento", mesaRegalos.IdEvento);
                parameters.Add("@Nombre", mesaRegalos.Nombre);
                parameters.Add("@Descripcion", mesaRegalos.Descripcion);
                parameters.Add("@Url", mesaRegalos.Url);

                // Ejecutar el procedimiento almacenado y capturar el ID insertado
                int idMesaRegalos = db.QuerySingle<int>(
                    "sp_MesaRegalos_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return idMesaRegalos;
            }
        }

        /// <summary>
        /// Retrieves gift tables from the database based on the specified filters.
        /// </summary>
        /// <param name="idMesaRegalos">The ID of the gift table to filter by (optional).</param>
        /// <param name="idEvento">The ID of the event to filter by (optional).</param>
        /// <param name="nombre">The name of the gift table to filter by (optional).</param>
        /// <param name="activo">The active status of the gift table to filter by (optional).</param>
        /// <returns>A list of gift tables that match the specified filters.</returns>
        public IEnumerable<MesaRegalos> Get(MesaRegalos mesaRegalos)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdMesaRegalos", mesaRegalos.IdMesaRegalos);
                parameters.Add("@IdEvento", mesaRegalos.IdEvento);
                parameters.Add("@Nombre", mesaRegalos.Nombre);
                parameters.Add("@Activo", mesaRegalos.Activo);

                return db.Query<MesaRegalos>("sp_MesaRegalos_Get", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Updates an existing gift table in the database.
        /// </summary>
        /// <param name="idMesaRegalos">The ID of the gift table to be updated.</param>
        /// <param name="idEvento">The ID of the event associated with the gift table (optional).</param>
        /// <param name="nombre">The name of the gift table (optional).</param>
        /// <param name="descripcion">The description of the gift table (optional).</param>
        /// <param name="url">The URL of the gift table (optional).</param>
        /// <param name="activo">Indicates if the gift table is active (optional).</param>
        public void Patch(MesaRegalos mesaRegalos)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdMesaRegalos", mesaRegalos.IdMesaRegalos);
                parameters.Add("@IdEvento", mesaRegalos.IdEvento);
                parameters.Add("@Nombre", mesaRegalos.Nombre);
                parameters.Add("@Descripcion", mesaRegalos.Descripcion);
                parameters.Add("@Url", mesaRegalos.Url);
                parameters.Add("@Activo", mesaRegalos.Activo);

                db.Execute("sp_MesaRegalos_Patch", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
