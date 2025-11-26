using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz.hub.Application.DTOs.CacheDTOs;
using quiz.hub.Application.DTOs.CasheDTOs;
using quiz.hub.Application.Interfaces.IServices.ICacheServices;

namespace quiz.hub.API.Controllers
{
    [Route("api/quiz-session")]
    [ApiController]
    public class QuizSessionController : ControllerBase
    {
        private readonly ICandidateSessionService _sessionService;

        public QuizSessionController(ICandidateSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveAnswer([FromBody] SaveAnswerDTO request)
        {
            await _sessionService.SaveAnswerAsync(request);
            return Ok(new { saved = true });
        }

        [HttpGet("{candidateId}/{quizId:guid}")]
        public async Task<IActionResult> GetAnswers(Guid candidateId, Guid quizId)
        {
            var answers = await _sessionService.GetAnswersAsync(candidateId, quizId);
            return Ok(answers);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitQuiz([FromBody] SubmitQuizDTO request, CancellationToken ct)
        {
            var success = await _sessionService.SubmitQuizAsync(request.CandidateId, request.QuizId, ct);

            if (!success)
                return BadRequest("No answers found for submission.");

            return Ok(new { submitted = true });
        }


        [HttpPost("clear")]
        public async Task<IActionResult> ClearSession(
            [FromQuery] Guid candidateId,[FromQuery] Guid quizId)
        {
            await _sessionService.ClearSessionAsync(candidateId, quizId);
            return Ok(new { cleared = true });
        }
    }
}

//{
//    "quizId": "b1d4431e-f107-4f11-a51c-beb2239f05be",
//  "candidateId": "58ee4c32-058f-450b-ad97-779a27ffa42e"
//}