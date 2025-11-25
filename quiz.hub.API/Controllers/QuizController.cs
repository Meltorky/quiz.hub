using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz.hub.Application.DTOs.QuizDTOs;

namespace quiz.hub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizController(IQuizService quizService, UserManager<ApplicationUser> userManager)
        {
            _quizService = quizService;
            _userManager = userManager;
        }

        // get all
        /// <summary>
        /// Get all quizzez, for Position Select one [ Admin | Host | Candidate ]
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<QuizDTO>>> GetAll([FromQuery] GetAllDTO dto, CancellationToken token)
        {
            var result = await _quizService.GetAll(dto.userId, dto.Position, dto.PageNumber??1, dto.PageSize??20, token);
            return Ok(result);
        }

        // create
        [HttpPost("creat")]
        public async Task<ActionResult<CreateQuizResultDTO>> Create([FromHeader] string userId, [FromBody] CreateQuizDTO dto, CancellationToken token)
        {
            var result = await _quizService.CreateQuiz(userId, dto, token);
            return Ok(result);
        }

        // publish
        [HttpPost("publish")]
        public async Task<ActionResult<QuizDTO>> Publish([FromBody] CreateQuizResultDTO dto, CancellationToken token)
        {
            var result = await _quizService.PublishQuiz(dto, token);
            return Ok(result);
        }

        // activate
        [HttpPost("activate")]
        public async Task<ActionResult<bool>> Activate([FromBody] CreateQuizResultDTO dto, CancellationToken token)
        {
            var result = await _quizService.ActivateQuiz(dto, token);
            return Ok(result);
        }

        // de-activate
        [HttpPost("de-activate")]
        public async Task<ActionResult<bool>> Deactivate([FromBody] CreateQuizResultDTO dto, CancellationToken token)
        {
            var result = await _quizService.DeactivateQuiz(dto, token);
            return Ok(result);
        }


        // calc abg score
        [HttpPost("calculate-avg-score")]
        public async Task<ActionResult<double>> Deactivate([FromQuery] Guid id, CancellationToken token)
        {
            var result = await _quizService.CalcAvgScore(id, token);
            return Ok(result);
        }

        // edit
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditQuizDTO dto, CancellationToken token)
        {
            await _quizService.EditQuiz(dto, token);
            return Ok();
        }

        // remove
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromBody] CreateQuizResultDTO dto, CancellationToken token)
        {
            await _quizService.RemoveQuiz(dto, token);
            return NoContent();
        }
    }
}
