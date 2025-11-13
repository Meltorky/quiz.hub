using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class CandidateAnswer
    {
        public Guid QuizId { get; set; }                      // part of composite key
        public Guid CandidateId { get; set; } = default!;     // part of composite key
        public Guid QuestionId { get; set; }                  // part of composite key
        public Guid AnswerId { get; set; }                    // chosen answer
        public bool IsTrue { get; set; }
        public double AnswerScore { get; set; }

        // Navigation
        public QuizCandidate QuizCandidate { get; set; } = default!;
        public Question Question { get; set; } = null!;
        public Answer Answer { get; set; } = null!;
    }

}
