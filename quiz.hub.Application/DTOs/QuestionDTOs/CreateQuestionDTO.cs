using Microsoft.AspNetCore.Http;
using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class CreateQuestionDTO
    {
        [JsonIgnore]
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
