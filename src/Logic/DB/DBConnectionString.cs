
namespace Logic.DB
{
    public class DBConnectionString
    {
        
        public string ConnectionString
        {
            get;
            private set;
        }

        public DBConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

    }
}