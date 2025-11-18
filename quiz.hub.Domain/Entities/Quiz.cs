using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quiz.hub.Domain.Entities
{
    public class Quiz
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Host))]
        public Guid HostId { get; set; }

        [MaxLength(7)]
        public string ConnectionCode { get; set; } = default!; // Unique join code

        public string Title { get; set; } = default!;
        public string? Description { get; set; }       
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }

        [Range(1,360)] // max of 6 hours
        public double DurationInMinutes { get; set; }
        public bool IsActive { get; set; }
        public double AverageScore { get; set; } 
        public int QuestionsNumber => Questions.Count;
        public double TotalScore => Questions.Select(x => x.Score).Sum();

        // nav props
        public Host Host { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizCandidate> QuizCandidates { get; set; } = new List<QuizCandidate>();
    }
}
