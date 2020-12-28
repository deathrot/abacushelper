using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Logic.Interfaces
{
    public interface IConnectionUtility
    {

        /// <summary>
        /// Gets the IDBConnection...
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();


    }
}
