using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace Logic.DB
{
    public static class DBUtility
    {
        public static async Task<IEnumerable<T>> GetDataFromTable<T>(Interfaces.IConnectionUtility connUtility, string tableName, Dictionary<string, object> parameters)
            where T : class, Interfaces.IDBEntity
        {
            if (string.IsNullOrEmpty(tableName))
                return null;

            using (var connection = connUtility.GetConnection())
            {
                var data = await Dapper.SqlMapper.QueryAsync<T>(connection, $"Select * From {tableName}", parameters);

                return data;
            }
        }

        public static async Task<IEnumerable<T>> GetData<T>(Interfaces.IConnectionUtility connUtility, string sql, Dictionary<string, object> parameters)
            where T : class, Interfaces.IDBEntity
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            using (var connection = connUtility.GetConnection())
            {
                return await GetData<T>(connection, sql, parameters);
            }
        }

        public static async Task<IEnumerable<T>> GetData<T>(System.Data.IDbConnection connection, string sql, Dictionary<string, object> parameters)
            where T : class, Interfaces.IDBEntity
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            var data = await Dapper.SqlMapper.QueryAsync<T>(connection, sql, parameters);

            return data;
        }

        public static async Task<T> GetScalar<T>(Interfaces.IConnectionUtility connUtility, string sql, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(sql))
                return default(T);

            return await GetScalar<T>(connUtility.GetConnection(), sql, parameters);
        }
        
        public static async Task<T> GetScalar<T>(System.Data.IDbConnection connection, string sql, Dictionary<string, object> parameters)
        {
            if (string.IsNullOrEmpty(sql))
                return default(T);

            var data = await Dapper.SqlMapper.ExecuteScalarAsync<T>(connection, sql, parameters);

            return data;
        }

        public static async Task<object> Insert<T>(Interfaces.IConnectionUtility connUtility, IEnumerable<T> entitesToInsert)
            where T : class, Interfaces.IDBEntity
        {
            object result = 0;

            if (entitesToInsert == null || entitesToInsert.Count() == 0)
                return result;

            using(var connection = connUtility.GetConnection())
            {
               result = await Insert<T>(connection, entitesToInsert);
            }

            return result;
        }

        public static async Task<object> Insert<T>(System.Data.IDbConnection connection, IEnumerable<T> entitesToInsert)
            where T : class, Interfaces.IDBEntity
        {
            int result = 0;

            if (entitesToInsert == null || entitesToInsert.Count() == 0)
                return result;

            foreach (var entity in entitesToInsert)
            {
                entity.modified_on = DateTime.UtcNow;
            }

            foreach (var entity in entitesToInsert)
            {
                result += await connection.InsertAsync<T>(entity);
            }

            return result;
        }

        public static async Task<bool> Delete<T>(Interfaces.IConnectionUtility connUtility, IEnumerable<T> entitiesToDelete)
            where T : class, Interfaces.IDBEntity
        {
            if (entitiesToDelete == null || entitiesToDelete.Count() == 0)
                return false;

            foreach(var entity in entitiesToDelete)
            {
                entity.is_deleted = true;
                entity.modified_on = DateTime.UtcNow;
            }

            using (var conn = connUtility.GetConnection())
            {
                bool result = true;

                foreach (var entity in entitiesToDelete)
                {
                    result = result && await conn.UpdateAsync<T>(entity);
                }

                return result;
            }
        }

        public static async Task<bool> Update<T>(Interfaces.IConnectionUtility connUtility, IEnumerable<T> entitiesToUpdate)
            where T : class, Interfaces.IDBEntity
        {
            if (entitiesToUpdate == null || entitiesToUpdate.Count() == 0)
                return false;
            
            foreach (var entity in entitiesToUpdate)
            {
                entity.modified_on = DateTime.UtcNow;
            }

            using (var conn = connUtility.GetConnection())
            {
                bool result = true;

                foreach (var entity in entitiesToUpdate)
                {
                    result = result && await conn.UpdateAsync<T>(entity);
                }
                
                return result;
            }
        }
    }
}