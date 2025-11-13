using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsPublished => PublishedAt < DateTime.UtcNow;

        [Range(1,360)] // max of 6 hours
        public double DurationInMinutes { get; set; }
        public bool IsActive => DateTime.UtcNow < PublishedAt.AddMinutes(DurationInMinutes);
        public double SuccessRate { get; set; } 
        public int QuestionsNumber { get; set; } 

        // nav props
        public Host Host { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizCandidate> QuizCandidates { get; set; } = new List<QuizCandidate>();
    }
}
