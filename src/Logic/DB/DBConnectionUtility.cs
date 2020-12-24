using MySql.Data.MySqlClient;
using System.Data;

namespace Logic.DB
{
    public class BaseDBConnectionUtility : Interfaces.IConnectionUtility
    {

        /// <summary>
        /// Connection String object
        /// </summary>
        static DBConnectionString connectionString = null;
        
        public static void Initialize(DBConnectionString connStr)
        {
            connectionString = connStr;
        }

        public IDbConnection GetConnection()
        {
           var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString.BaseDBConnectionString);

           return connection;
        }

    }
}