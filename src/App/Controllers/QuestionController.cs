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
    public class QuestionController : ControllerBase
    {

        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logic.DBModels.QuestionEntity>>> GetQuestions(string page)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<Logic.DBModels.QuestionEntity>> GetRandomQuestion(string level)
        {
            return null;
        }

        [HttpGet]
        public ActionResult SaveQuestion(Logic.DBModels.QuestionEntity questionToSave)
        {
            return new OkResult();
        }
    }
}
