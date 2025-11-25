using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz.hub.Application.DTOs.QuestionDTOs;

namespace quiz.hub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // create
        [HttpPost("create")]
        public async Task<ActionResult<QuestionWithAnswersDTO>> Create([FromBody] CreateQuestionDTO dto, CancellationToken token)
        {
            var result = await _questionService.CreatQuestionWithAnswers(dto,token);
            return Ok(result);
        }


        // edit
        [HttpPut("edit")]
        public async Task<ActionResult<QuestionDTO>> Edit([FromBody] EditQuestionDTO dto, CancellationToken token)
        {
            var result = await _questionService.EditQuestion(dto, token);
            return Ok(result);
        }

        
        // remove
        [HttpDelete("remove")]
        public async Task<IActionResult> Remove([FromQuery] Guid QuestionID, CancellationToken token)
        {
            await _questionService.RemoveQuestionWithAnsers(QuestionID, token);
            return NoContent();
        }
    }
}
