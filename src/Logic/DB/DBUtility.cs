using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DB
{
    /// <summary>
    /// DB Utility to load 
    /// </summary>
    public static class DBUtility
    {
        public static async Task<IEnumerable<T>> GetDataFromTable<T>(string tableName, Dictionary<string, object> parameters)
        {
            using (var connection = DBConnectionUtility.GetConnection())
            {
                var data = await Dapper.SqlMapper.QueryAsync<T>(connection, $"Select * From {tableName}", parameters);

                return data;
            }
        }

        public static async Task<IEnumerable<T>> GetData<T>(string sql, Dictionary<string, object> parameters)
        {
            var data = await Dapper.SqlMapper.QueryAsync<T>(DBConnectionUtility.GetConnection(), sql, parameters);

            return data;
        }

        public static async Task<T> GetScalar<T>(string sql, Dictionary<string, object> parameters)
        {
            var data = await Dapper.SqlMapper.ExecuteScalarAsync<T>(DBConnectionUtility.GetConnection(), sql, parameters);

            return data;
        }

    }
}
