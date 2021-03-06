using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Business
{

    [ServicesExtensions.Service]
    public class SessionBL : Interfaces.ISessionBL
    {

        private readonly Interfaces.ISessionCacheProvider _sessionCacheProvider;

        public SessionBL(Interfaces.ISessionCacheProvider sessionCacheProvider)
        {
            _sessionCacheProvider = sessionCacheProvider;
        }

        public async Task<bool> ValidateAndRenewSession(DB.StudentDBConnectionUtility connectionUtility, ViewModels.SessionVM sessionVM)
        {
            using (var connection = connectionUtility.GetConnection())
            {
               var result = await IsSessionValid(connection, sessionVM);

                if ( result)
                {
                    Providers.UserProvider provider = new Providers.UserProvider();
                    DateTime? dt = await provider.RenewSession(connection, sessionVM, Constants.Constants.SESSION_TIMEOUT_IN_SECONDS);

                    if ( dt.HasValue)
                    {
                        sessionVM.LastActivityTime = DateTime.UtcNow;
                        sessionVM.NextLoginTimeout = dt.Value;

                        _sessionCacheProvider.AddSessionModel(sessionVM);

                        return true;
                    }
                }
            }

            _sessionCacheProvider.RemoveSessionModel(sessionVM.SessionToken);
            return false;
        }

        public async Task<bool> IsSessionValid(System.Data.IDbConnection connection, ViewModels.SessionVM sessionVM)
        {
            if (sessionVM == null || sessionVM.NextLoginTimeout >= DateTime.UtcNow)
                return false;

            var sessionModel = _sessionCacheProvider.GetCachedSession(sessionVM.SessionToken);

            if (sessionModel == null)
            {
                Providers.UserProvider provider = new Providers.UserProvider();
                DBModels.SessionsEntity session = await provider.GetSessionModel(connection, sessionVM.SessionToken);

                if (session == null)
                {
                    return false;
                }

                sessionModel = Mappers.ObjectMapper.Instance.Mapper.Map<ViewModels.SessionVM>(session);
            }

            if (sessionModel.Equals(sessionVM) && sessionModel.NextLoginTimeout < DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }

    }

}
