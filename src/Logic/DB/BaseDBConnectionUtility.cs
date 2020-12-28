using MySql.Data.MySqlClient;
using System.Data;

namespace Logic.DB
{
    public class BaseDBConnectionUtility : Interfaces.IConnectionUtility
    {

        public string connectionString {get; set;}
        public BaseDBConnectionUtility(string connectionString)
        {
            this.connectionString = connectionString;
        }
        
        public IDbConnection GetConnection()
        {
           var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

           return connection;
        }

    }
}