using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.CasheDTOs
{
    public class SaveAnswerDTO
    {
        public Guid QuizId { get; set; }
        public Guid CandidateId { get; set; } = default!;
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}
