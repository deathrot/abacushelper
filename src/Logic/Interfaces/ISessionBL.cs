using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    /// <summary>
    /// Session interface
    /// </summary>
    [Logic.ServicesExtensions.Service]
    public interface ISessionBL
    {

        /// <summary>
        /// Validate and Renew Session
        /// </summary>
        /// <param name="connectionUtility"></param>
        /// <param name="sessionVM"></param>
        /// <returns></returns>
        Task<bool> ValidateAndRenewSession(Logic.DB.StudentDBConnectionUtility connectionUtility, Logic.ViewModels.SessionVM sessionVM);

        /// <summary>
        /// Is Session Valid
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sessionVM"></param>
        /// <returns></returns>
        Task<bool> IsSessionValid(System.Data.IDbConnection connection, Logic.ViewModels.SessionVM sessionVM);


    }
}
