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
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string CreateUserAccount(Logic.ViewModels.StudentVM studentToCreate)
        {
            return $"{Dns.GetHostName()}";
        }

        [HttpPost]
        public bool CheckRegisteredEmail(string emailAddressToCheck)
        {
            return false;
        }
    }
}
