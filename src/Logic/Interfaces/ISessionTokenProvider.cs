using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{

    [Logic.ServicesExtensions.Service]
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
        bool IsSessionValid(Logic.ViewModels.SessionVM session, string token);

    }
}
