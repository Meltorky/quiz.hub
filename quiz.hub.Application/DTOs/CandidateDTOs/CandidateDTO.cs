using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.CandidateDTOs
{
    public class CandidateDTO
    {
        public Guid QuizId { get; set; }                  
        public Guid CandidateId { get; set; }  
        public string Email { get; set; } = string.Empty!;
        public double TotalScore { get; set; }
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
    }
}
