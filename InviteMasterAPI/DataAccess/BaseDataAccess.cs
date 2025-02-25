namespace InviteMasterAPI.DataAccess
{
    public class BaseDataAccess
    {
        protected readonly string _connectionString;

        public BaseDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
