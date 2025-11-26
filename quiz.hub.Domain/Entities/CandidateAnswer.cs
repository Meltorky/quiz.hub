using quiz.hub.Domain.Identity;

namespace quiz.hub.Domain.Entities
{
    public class CandidateAnswer
    {
        public Guid QuizId { get; set; }                      // part of composite key
        public string CandidateId { get; set; } = default!;   // part of composite key
        public Guid QuestionId { get; set; }                  // part of composite key
        public Guid AnswerId { get; set; }                    // chosen answer
        public bool IsTrue { get; set; }
        public double AnswerScore { get; set; }

        // Navigation
        public ApplicationUser Candidate { get; set; } = null!;
        public QuizCandidate QuizCandidate { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public Answer Answer { get; set; } = null!;
    }

}
