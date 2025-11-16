using quiz.hub.Application.DTOs.AnswerDTOs;
using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class EditQuestionWithAnswersDTO
    {
        [Required]
        public EditQuestionDTO questionDTO { get; set; } = new();

        [Required]
        public List<AnswerDTO> answerDTOs { get; set; } = new();
    }
}
