using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Application.DTOs.QuestionDTOs;

namespace quiz.hub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        // create
        [HttpPost("create")]
        public async Task<ActionResult<AnswerDTO>> Create([FromBody] CreateAnswerDTO dto, CancellationToken token)
        {
            var result = await _answerService.CreateAnswer(dto, token);
            return Ok(result);
        }


        // edit
        [HttpPut("edit")]
        public async Task<ActionResult<AnswerDTO>> Edit([FromBody] AnswerDTO dto, CancellationToken token)
        {
            var result = await _answerService.EditAnswer(dto, token);
            return Ok(result);
        }


        // remove
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] Guid AnswerId, CancellationToken token)
        {
            await _answerService.DeleteAnswer(AnswerId, token);
            return NoContent();
        }
    }
}
