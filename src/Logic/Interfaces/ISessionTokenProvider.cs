using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ISessionTokenProvider
    {

        /// <summary>
        /// Creates the unique token specific for the current session...
        /// </summary>
        /// <returns></returns>
        string GetToken();

        /// <summary>
        /// Confirms if session is valid
        /// </summary>
        /// <returns></returns>
        bool IsSessionValid(string token);

    }
}
