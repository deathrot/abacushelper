using MySql.Data.MySqlClient;
using System.Data;

namespace Logic.DB
{
    public class StudentDBConnectionUtility : Interfaces.IConnectionUtility
    {

        string connectionString;
        public StudentDBConnectionUtility(string connectionString)
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