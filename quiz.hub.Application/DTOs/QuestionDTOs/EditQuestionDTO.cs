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
        [Required]
        public Guid QuizId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public string? ImageBase64 { get; set; }
        public double Score { get; set; }
    }
}
