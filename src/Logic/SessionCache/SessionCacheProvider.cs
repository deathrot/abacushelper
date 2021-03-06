using Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.SessionCache
{
    /// <summary>
    /// Session Cache Provider
    /// </summary>
    [Logic.ServicesExtensions.Service]
    public class SessionCacheProvider : Interfaces.ISessionCacheProvider
    {

        System.Collections.Concurrent.ConcurrentDictionary<string, ViewModels.SessionVM> _sessionCache;

        public SessionCacheProvider()
        {
            _sessionCache = new System.Collections.Concurrent.ConcurrentDictionary<string, ViewModels.SessionVM>();
        }

        public bool AddSessionModel(SessionVM session)
        {
            var sessionInDict = _sessionCache.AddOrUpdate(session.SessionToken, session ,(key, s) =>
            {
                s.NextLoginTimeout = session.NextLoginTimeout;
                s.LastActivityTime = session.LastActivityTime;

                return s;
            });

            return sessionInDict != null;
        }

        public SessionVM GetCachedSession(string sessionToken)
        {
            ViewModels.SessionVM session;
            _sessionCache.TryGetValue(sessionToken, out session);
            return session;
        }

        public bool RemoveSessionModel(string sessionToken)
        {
            ViewModels.SessionVM session;
            return _sessionCache.TryRemove(sessionToken, out session);
        }
    }
}
