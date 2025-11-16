using Microsoft.AspNetCore.Http;
using quiz.hub.Application.DTOs.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class EditQuestionDTO
    {
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }

        [Required]
        public double Score { get; set; }
    }
}
