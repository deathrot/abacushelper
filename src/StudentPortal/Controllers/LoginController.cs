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

        public LoginController(ILogger<LoginController> logger, Logic.DB.StudentDBConnectionUtility connectionUtility)
        {
            _logger = logger;
            _connectionUtility = connectionUtility;
        }

        [HttpPost]
        public async Task<Logic.ViewModels.AuthenticateResultVM> InitiateLogin(Logic.Models.LoginRequest request)
        {
            if ( request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password)){
                return new Logic.ViewModels.AuthenticateResultVM();
            }

            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            var result = await provider.Login(_connectionUtility, request.Email, request.Password, new Logic.Models.LoginSessionProvider());

            return result;
        }
    }
}
