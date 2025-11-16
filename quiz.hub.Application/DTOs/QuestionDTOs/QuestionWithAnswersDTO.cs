using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class QuestionWithAnswersDTO
    {
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public double Score { get; set; }
        public int Order { get; set; }
        public List<AnswerDTO> answers { get; set; } = new();
    }
}
