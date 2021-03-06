using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentPortal.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Logic.DB.StudentDBConnectionUtility _connectionUtility;
        private readonly Logic.Interfaces.ISessionCacheProvider _sessionCacneProvider;

        public LoginController(ILogger<LoginController> logger, Logic.DB.StudentDBConnectionUtility connectionUtility, 
                                Logic.Interfaces.ISessionCacheProvider sessionCacneProvider)
        {
            _logger = logger;
            _connectionUtility = connectionUtility;
            _sessionCacneProvider = sessionCacneProvider;
        }

        [HttpPost]
        public async Task<Logic.ViewModels.AuthenticateResultVM> InitiateLogin(Logic.Models.LoginRequest request)
        {
            if ( request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password)){
                return new Logic.ViewModels.AuthenticateResultVM();
            }

            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            var result = await provider.Login(_connectionUtility, request.Email, request.Password, new Logic.Models.LoginSessionProvider());

            if ( result.ResultType == Logic.Enums.AuthenticateResultType.Success)
            {
                _sessionCacneProvider.AddSessionModel(result.Session);
            }

            return result;
        }
        

        [HttpPost]
        public async Task<bool> Logout(Logic.Models.LogoutRequest request)
        {
            if ( request == null || request.SessionToken == null || string.IsNullOrEmpty(request.SessionToken)){
                return false;
            }

            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            return await provider.Logout(_connectionUtility, request.SessionToken);
        }
    }
}
