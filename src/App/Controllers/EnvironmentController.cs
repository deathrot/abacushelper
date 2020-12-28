using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnvironmentController : ControllerBase
    {

        private readonly Logic.DB.BaseDBConnectionUtility _connectionUtility;

        public EnvironmentController(Logic.DB.BaseDBConnectionUtility connectionUtility)
        {
            _connectionUtility = connectionUtility;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            await Task.Delay(0);
            return _connectionUtility.connectionString;
        }        
    }
}
