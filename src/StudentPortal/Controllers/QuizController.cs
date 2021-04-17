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
    public class QuizController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Logic.DB.StudentDBConnectionUtility _connectionUtility;
        private readonly Logic.Interfaces.ISessionCacheProvider _sessionCacneProvider;
        private readonly Logic.Interfaces.IQuizProvider _quizProvider;

        public QuizController(ILogger<LoginController> logger, Logic.DB.StudentDBConnectionUtility connectionUtility, 
                                Logic.Interfaces.ISessionCacheProvider sessionCacneProvider,
                                Logic.Interfaces.IQuizProvider quizProvider)
        {
            _logger = logger;
            _quizProvider = quizProvider;
            _connectionUtility = connectionUtility;
            _sessionCacneProvider = sessionCacneProvider;
        }
        
        [HttpGet]
        public async Task<Logic.ViewModels.Quiz> FetchQuiz()
        {
            return await _quizProvider.GetQuiz(0, 3);
        }
    }
}
