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
    public class AbacusLevelController : ControllerBase
    {

        private readonly ILogger<QuestionController> _logger;

        public AbacusLevelController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logic.ViewModels.AbacusLevelVM>>> Get()
        {
            return null;
        }
    }
}
