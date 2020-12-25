﻿using System;
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

        private readonly Logic.DB.BaseDBConnectionUtility _connectionUtility;

        public QuestionsController(ILogger<QuestionsController> logger, Logic.DB.BaseDBConnectionUtility connectionUtility)
        {
            _logger = logger;
            _connectionUtility = connectionUtility;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logic.ViewModels.QuestionVM>>> Get()
        {
            await Task.Delay(0);

            return new Logic.ViewModels.QuestionVM[]{};
        }

        [HttpPost]
        public async Task<ActionResult<Logic.Models.DBSaveResult>> Save(Logic.Models.QuestionSaveResult request)
        {
            await Task.Delay(0);
            return null;
        }
    }
}
