using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.AnswerDTOs
{
    public class CreateAnswerDTO
    {
        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public bool IsCorrect { get; set; }
    }
}
