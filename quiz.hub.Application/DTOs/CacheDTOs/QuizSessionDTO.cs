using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.CacheDTOs
{
    public class QuizSessionDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CandidateId { get; set; } = default!;
        public Guid QuizId { get; set; }
        public ICollection<CandidateAnswer> Answers { get; set; } = new List<CandidateAnswer>();
    }
}
