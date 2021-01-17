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
    public class CreateAccountController : ControllerBase
    {
        private readonly ILogger<CreateAccountController> _logger;
        private readonly Logic.Interfaces.IConnectionUtility _connectionUtility;

        public CreateAccountController(ILogger<CreateAccountController> logger, Logic.DB.StudentDBConnectionUtility connectionUtility)
        {
            _logger = logger;
            _connectionUtility = connectionUtility;
        }

        [HttpPost]
        public async Task<bool> CheckDisplayNameAvaliability(string displayName)
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            var result = await provider.CheckDisplayNameAlreadyUsed(_connectionUtility, displayName);
            
            return !result;
        }

        [HttpPost]
        public async Task<bool> CheckEmailAvailiability(string emailAddressToCheck)
        {
            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            var result = await provider.CheckEmailAlreadyRegistered(_connectionUtility, emailAddressToCheck);
            
            return !result;
        }

        [HttpPost]
        public async Task<Logic.Validators.StudentValidationResult> CreateAccount([FromBody]Logic.ViewModels.StudentVM studentToCreate)
        {
            if ( studentToCreate == null)
            {
                return new Logic.Validators.StudentValidationResult() { ValidationSuccess = false};
            }
            
            Logic.Providers.StudentProvider provider = new Logic.Providers.StudentProvider();
    
            var result = await provider.SaveStudent(_connectionUtility, studentToCreate);
            
            return result;
        }
    }
}
