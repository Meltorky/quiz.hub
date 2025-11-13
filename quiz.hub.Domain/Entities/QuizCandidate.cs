using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class QuizCandidate
    {
        public Guid QuizId { get; set; }                      // PK part 1 (FK -> Quiz)
        public Guid CandidateId { get; set; } = default!;     // PK part 2 (FK -> Candidate)
        
        public double TotalScore { get; set; }
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Quiz Quiz { get; set; } = null!;
        public Candidate Candidate { get; set; } = null!;
        public ICollection<CandidateAnswer> CandidateAnswers { get; set; } = new List<CandidateAnswer>();
    }

}
