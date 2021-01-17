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
        public async Task<Logic.Enums.AvailiabilityEnum> CheckDisplayNameAvaliability(Logic.Models.AvailiabilityInput displayName)
        {
            if ( string.IsNullOrEmpty(displayName.EntityToCheck) || string.IsNullOrEmpty(displayName.EntityToCheck.Trim()))
                return Logic.Enums.AvailiabilityEnum.None;

            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            try
            {
                var result = await provider.CheckDisplayNameAlreadyUsed(_connectionUtility, displayName.EntityToCheck);

                if (result)
                {
                    return Logic.Enums.AvailiabilityEnum.NotAvailable;
                }

                return Logic.Enums.AvailiabilityEnum.Available;
            }
            catch{
                return Logic.Enums.AvailiabilityEnum.Unknown;
            }
        }

        [HttpPost]
        public async Task<Logic.Enums.AvailiabilityEnum> CheckEmailAvailiability(Logic.Models.AvailiabilityInput emailAddressToCheck)
        {
            if ( string.IsNullOrEmpty(emailAddressToCheck.EntityToCheck) || string.IsNullOrEmpty(emailAddressToCheck.EntityToCheck.Trim()))
                return Logic.Enums.AvailiabilityEnum.None;

            Logic.Providers.UserProvider provider = new Logic.Providers.UserProvider();
            try
            {
                var result = await provider.CheckEmailAlreadyRegistered(_connectionUtility, emailAddressToCheck.EntityToCheck);

                if (result)
                {
                    return Logic.Enums.AvailiabilityEnum.NotAvailable;
                }

                return Logic.Enums.AvailiabilityEnum.Available;
            }
            catch{
                return Logic.Enums.AvailiabilityEnum.Unknown;
            }
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