using Microsoft.AspNetCore.Http;
using quiz.hub.Application.DTOs.AnswerDTOs;
using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class CreateQuestionDTO
    {
        [Required]
        public Guid QuizId { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        [Required]
        public double Score { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "You must provide at least 2 answers.")]
        [MaxLength(10, ErrorMessage = "Maximum 10 answers allowed.")]
        public List<CreateAnswerDTO> answerDTOs { get; set; } = new();
    }
}
