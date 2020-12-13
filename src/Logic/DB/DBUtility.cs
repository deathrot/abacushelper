using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DB
{
    /// <summary>
    /// DB Utility to load 
    /// </summary>
    public class DBUtility
    {

        public async Task<IEnumerable<T>> GetData<T>(string sql, Dictionary<string, object> parameters)
        {
            var data = await Dapper.SqlMapper.QueryAsync<T>(DBConnectionUtility.GetConnection(), sql, parameters);

            return data;
        }

        public async Task<T> GetScalar<T>(string sql, Dictionary<string, object> parameters)
        {
            var data = await Dapper.SqlMapper.ExecuteScalarAsync<T>(DBConnectionUtility.GetConnection(), sql, parameters);

            return data;
        }

    }
}
