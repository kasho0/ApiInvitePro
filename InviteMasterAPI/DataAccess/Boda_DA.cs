using System.Data;
using System.Data.SqlClient;
using Dapper;
using InviteMasterAPI.Model;

namespace InviteMasterAPI.DataAccess
{
    public class Boda_DA : BaseDataAccess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Boda_DA"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Boda_DA(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Inserts a new wedding record into the database.
        /// </summary>
        /// <param name="boda">The wedding details to be inserted.</param>
        public void Insert(Boda boda)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdEvento", boda.IdEvento);
                parameters.Add("@Civil", boda.Civil);
                parameters.Add("@CivilLugar", boda.CivilLugar);
                parameters.Add("@CivilDireccion", boda.CivilDireccion);
                parameters.Add("@CivilUbicacion", boda.CivilUbicacion);
                parameters.Add("@CivilHorario", boda.CivilHorario);
                parameters.Add("@Iglesia", boda.Iglesia);
                parameters.Add("@IglesiaLugar", boda.IglesiaLugar);
                parameters.Add("@IglesiaDireccion", boda.IglesiaDireccion);
                parameters.Add("@IglesiaUbicacion", boda.IglesiaUbicacion);
                parameters.Add("@IglesiaHorario", boda.IglesiaHorario);
                parameters.Add("@Local", boda.Local);
                parameters.Add("@LocalLugar", boda.LocalLugar);
                parameters.Add("@LocalDireccion", boda.LocalDireccion);
                parameters.Add("@LocalUbicacion", boda.LocalUbicacion);
                parameters.Add("@LocalHorario", boda.LocalHorario);
                parameters.Add("@NoNinos", boda.NoNinos);
                parameters.Add("@Padrinos", boda.Padrinos);
                parameters.Add("@SobreRegalo", boda.SobreRegalo);
                parameters.Add("@SobreRegaloTexto", boda.SobreRegaloTexto);
                parameters.Add("@Transferencia", boda.Transferencia);
                parameters.Add("@TransferenciaTexto", boda.TransferenciaTexto);
                parameters.Add("@TransferenciaDatos", boda.TransferenciaDatos);
                parameters.Add("@MesaRegalos", boda.MesaRegalos);
                parameters.Add("@MesaRegalosTexto", boda.MesaRegalosTexto);
                parameters.Add("@CatEtiqueta", boda.CatEtiqueta.ToString());

                db.Execute("sp_Boda_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Retrieves wedding records from the database based on the specified filters.
        /// </summary>
        /// <param name="boda"></param>
        /// <returns>A list of weddings that match the specified filters.</returns>
        public IEnumerable<Boda> Get(Boda boda)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdBoda", boda.IdBoda);
                parameters.Add("@IdEvento", boda.IdEvento);
                parameters.Add("@Civil", boda.Civil);
                parameters.Add("@CivilLugar", boda.CivilLugar);
                parameters.Add("@CivilDireccion", boda.CivilDireccion);
                parameters.Add("@CivilUbicacion", boda.CivilUbicacion);
                parameters.Add("@CivilHorario", boda.CivilHorario);
                parameters.Add("@Iglesia", boda.Iglesia);
                parameters.Add("@IglesiaLugar", boda.IglesiaLugar);
                parameters.Add("@IglesiaDireccion", boda.IglesiaDireccion);
                parameters.Add("@IglesiaUbicacion", boda.IglesiaUbicacion);
                parameters.Add("@IglesiaHorario", boda.IglesiaHorario);
                parameters.Add("@Local", boda.Local);
                parameters.Add("@LocalLugar", boda.LocalLugar);
                parameters.Add("@LocalDireccion", boda.LocalDireccion);
                parameters.Add("@LocalUbicacion", boda.LocalUbicacion);
                parameters.Add("@LocalHorario", boda.LocalHorario);
                parameters.Add("@NoNinos", boda.NoNinos);
                parameters.Add("@Padrinos", boda.Padrinos);
                parameters.Add("@SobreRegalo", boda.SobreRegalo);
                parameters.Add("@SobreRegaloTexto", boda.SobreRegaloTexto);
                parameters.Add("@Transferencia", boda.Transferencia);
                parameters.Add("@TransferenciaTexto", boda.TransferenciaTexto);
                parameters.Add("@TransferenciaDatos", boda.TransferenciaDatos);
                parameters.Add("@MesaRegalos", boda.MesaRegalos);
                parameters.Add("@MesaRegalosTexto", boda.MesaRegalosTexto);
                parameters.Add("@CatEtiqueta", boda.CatEtiqueta);
                parameters.Add("@Activo", boda.Activo);

                return db.Query<Boda>("sp_Boda_Get", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Updates an existing wedding event in the database.
        /// </summary>
        /// <param name="boda"></param>
        public void Patch(Boda boda)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IdBoda", boda.IdBoda);
                parameters.Add("@IdEvento", boda.IdEvento);
                parameters.Add("@Civil", boda.Civil);
                parameters.Add("@CivilLugar", boda.CivilLugar);
                parameters.Add("@CivilDireccion", boda.CivilDireccion);
                parameters.Add("@CivilUbicacion", boda.CivilUbicacion);
                parameters.Add("@CivilHorario", boda.CivilHorario);
                parameters.Add("@Iglesia", boda.Iglesia);
                parameters.Add("@IglesiaLugar", boda.IglesiaLugar);
                parameters.Add("@IglesiaDireccion", boda.IglesiaDireccion);
                parameters.Add("@IglesiaUbicacion", boda.IglesiaUbicacion);
                parameters.Add("@IglesiaHorario", boda.IglesiaHorario);
                parameters.Add("@Local", boda.Local);
                parameters.Add("@LocalLugar", boda.LocalLugar);
                parameters.Add("@LocalDireccion", boda.LocalDireccion);
                parameters.Add("@LocalUbicacion", boda.LocalUbicacion);
                parameters.Add("@LocalHorario", boda.LocalHorario);
                parameters.Add("@NoNinos", boda.NoNinos);
                parameters.Add("@Padrinos", boda.Padrinos);
                parameters.Add("@SobreRegalo", boda.SobreRegalo);
                parameters.Add("@SobreRegaloTexto", boda.SobreRegaloTexto);
                parameters.Add("@Transferencia", boda.Transferencia);
                parameters.Add("@TransferenciaTexto", boda.TransferenciaTexto);
                parameters.Add("@TransferenciaDatos", boda.TransferenciaDatos);
                parameters.Add("@MesaRegalos", boda.MesaRegalos);
                parameters.Add("@MesaRegalosTexto", boda.MesaRegalosTexto);
                parameters.Add("@CatEtiqueta", boda.CatEtiqueta.ToString());
                parameters.Add("@Activo", boda.Activo);

                db.Execute("sp_Boda_Patch", parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
