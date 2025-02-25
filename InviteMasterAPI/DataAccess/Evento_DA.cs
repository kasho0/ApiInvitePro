using System.Data;
using System.Data.SqlClient;
using Dapper;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.DataAccess
{
    public class Evento_DA : BaseDataAccess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Evento_DA"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Evento_DA(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Inserts a new event into the database.
        /// </summary>
        /// <param name="evento">The event to be inserted.</param>
        /// <returns>The ID of the inserted event.</returns>
        public int Insert(Evento evento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@NombreEvento", evento.NombreEvento);
                parameters.Add("@FechaEvento", evento.FechaEvento);
                parameters.Add("@CatEventoTipo", evento.CatEventoTipo.ToString());
                parameters.Add("@FotoPrincipalUrl", evento.FotoPrincipalUrl);
                parameters.Add("@FechaCreacion", evento.FechaCreacion);

                // Ejecutar el procedimiento almacenado y capturar el ID insertado
                int idEvento = db.QuerySingle<int>(
                    "sp_Evento_Insert",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return idEvento;
            }
        }

        /// <summary>
        /// Retrieves events from the database based on the specified filters.
        /// </summary>
        /// <param name="nombreEvento">The name of the event to filter by (optional).</param>
        /// <param name="fechaEvento">The date of the event to filter by (optional).</param>
        /// <param name="catEventoTipo">The type of the event to filter by (optional).</param>
        /// <param name="catEventoStatus">The status of the event to filter by (optional).</param>
        /// <returns>A list of events that match the specified filters.</returns>
        public IEnumerable<Evento> Get(Evento evento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdEvento", evento.IdEvento);
                parameters.Add("@NombreEvento", evento.NombreEvento);
                parameters.Add("@FechaEvento", evento.FechaEvento);
                parameters.Add("@CatEventoTipo", evento.CatEventoTipo.ToString());
                parameters.Add("@CatEventoStatus", evento.CatEventoStatus.ToString());

                return db.Query<Evento>("sp_Evento_Get", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        
        /// <summary>
        /// Updates an existing event in the database.
        /// </summary>
        /// <param name="idEvento">The ID of the event to be updated.</param>
        /// <param name="nombreEvento">The new name of the event (optional).</param>
        /// <param name="fechaEvento">The new date of the event (optional).</param>
        /// <param name="fotoPrincipalUrl">The new URL of the main photo of the event (optional).</param>
        /// <param name="catEventoStatus">The new status of the event (optional).</param>
        public void Patch(Evento evento)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                 var parameters = new DynamicParameters();
                parameters.Add("@IdEvento", evento.IdEvento);
                parameters.Add("@NombreEvento", evento.NombreEvento);
                parameters.Add("@FechaEvento", evento.FechaEvento);
                parameters.Add("@FotoPrincipalUrl", evento.FotoPrincipalUrl);
                parameters.Add("@CatEventoStatus", evento.CatEventoStatus.ToString());

                db.Execute("sp_Evento_Patch", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
