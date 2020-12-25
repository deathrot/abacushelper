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
    public class QuestionsController : ControllerBase
    {

        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logic.ViewModels.QuestionVM>>> Get()
        {
            await Task.Delay(0);
            return new Logic.ViewModels.QuestionVM[]{};
        }



        [HttpPost]
        public async Task<ActionResult<IEnumerable<Logic.ViewModels.QuestionVM>>> Save(IEnumerable<Logic.ViewModels.QuestionVM> entitesToUpdate,
                                                                                        IEnumerable<Logic.ViewModels.QuestionVM> entitesToDelete,
                                                                                        IEnumerable<Logic.ViewModels.QuestionVM> entitesToInsert)
        {
            await Task.Delay(0);
            return new Logic.ViewModels.QuestionVM[] { };
        }

        /*[HttpGet]
        public async Task<ActionResult<Logic.DBModels.QuestionEntity>> GetRandomQuestion(string level)
        {
            return null;
        }

        [HttpGet]
        public ActionResult SaveQuestion(Logic.DBModels.QuestionEntity questionToSave)
        {
            return new OkResult();
        }*/
    }
}
