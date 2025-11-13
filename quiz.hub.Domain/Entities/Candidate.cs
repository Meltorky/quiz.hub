using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class Candidate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;    // create non-clusterd index

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public bool IsUser => UserId != null;

        // Navigation
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<QuizCandidate> QuizCandidates { get; set; } = new List<QuizCandidate>();
    }
}
