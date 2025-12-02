using Microsoft.AspNetCore.Mvc;
using OpenTriviaApp.API.Models;
using OpenTriviaApp.API.Services;

namespace OpenTriviaApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TriviaController(ITriviaService service) : ControllerBase
    {
        private readonly ITriviaService _service = service;

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _service.FetchQuestionsAsync();
            return Ok(questions);
        }

        [HttpPost("checkanswers")]
        public IActionResult CheckAnswers([FromBody] List<UserAnswer> answers)
        {
            var result = _service.CheckAnswers(answers);
            return Ok(result);
        }
    }
}
