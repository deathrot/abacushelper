using MySql.Data.MySqlClient;

namespace Logic.DB
{
    public static class DBConnectionUtility
    {

        /// <summary>
        /// Connection String object
        /// </summary>
        static DBConnectionString connectionString = null;
        
        public static void Initialize(DBConnectionString connStr)
        {
            connectionString = connStr;
        }

        public static MySqlConnection GetConnection()
        {
           var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString.ConnectionString);

           return connection;
        }

    }
}