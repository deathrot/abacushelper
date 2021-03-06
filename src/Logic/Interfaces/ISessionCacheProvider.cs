using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    [Logic.ServicesExtensions.Service]
    public interface ISessionCacheProvider
    {

        /// <summary>
        /// Get cached session
        /// </summary>
        /// <param name="sessionToken"></param>
        ViewModels.SessionVM GetCachedSession(string sessionToken);

        /// <summary>
        /// Add session models
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        bool AddSessionModel(ViewModels.SessionVM session);

        /// <summary>
        /// Remove session model
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <returns></returns>
        bool RemoveSessionModel(string sessionToken);

    }
}
