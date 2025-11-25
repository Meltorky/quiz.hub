using System.ComponentModel.DataAnnotations;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class JoinQuizResultDTO
    {
        public string CandidateId { get; set; } = default!;
        public Guid quizId { get; set; } = default!;
        public string HostName { get; set; } = string.Empty;
        public string ConnectionCode { get; set; } = default!; // Unique join code
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime PublishedAt { get; set; }
        public double DurationInMinutes { get; set; }
        public bool IsActive { get; set; }
        public double TotalScore { get; set; }
        public int NumberOfQuestions { get; set; }
        public int CandiantesEnrolled { get; set; }

    }
}
