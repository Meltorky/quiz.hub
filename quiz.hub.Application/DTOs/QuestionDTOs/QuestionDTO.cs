using quiz.hub.Application.DTOs.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuestionDTOs
{
    public class QuestionDTO
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
