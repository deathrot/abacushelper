using System;
using System.Collections.Generic;
using System.Data;

namespace Logic.DB
{
    public static class SqlHelper
    {

        public static bool AddParameters(System.Data.IDbCommand command, System.Collections.Generic.Dictionary<string, object> parametersToAdd)
        {
            if (command == null || parametersToAdd == null || parametersToAdd.Count == 0)
                return false;

            foreach (KeyValuePair<string, object> param in parametersToAdd)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = param.Key;
                parameter.DbType = getDbType(param.Value);
                parameter.Value = (param.Value == null ? DBNull.Value : param.Value);
                command.Parameters.Add(parameter);
            }

            return true;
        }

        private static DbType getDbType(object value)
        {
            if (value == null)
                return DbType.String;

            if (value.GetType() == typeof(System.Int32))
            {
                return DbType.Int32;
            }

            if (value.GetType() == typeof(System.Decimal))
            {
                return DbType.Decimal;
            }

            if (value.GetType() == typeof(System.Int64))
            {
                return DbType.Int64;
            }

            if (value.GetType() == typeof(System.Double))
            {
                return DbType.Double;
            }

            if (value.GetType() == typeof(System.String))
            {
                return DbType.String;
            }
            
            if (value.GetType() == typeof(System.Boolean))
            {
                return DbType.Boolean;
            }
            
            if (value.GetType() == typeof(System.DateTime))
            {
                return DbType.DateTime;
            }

            return DbType.String;
        }

    }
}