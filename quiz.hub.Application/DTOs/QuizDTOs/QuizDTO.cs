using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class QuizDTO
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public string ConnectionCode { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public double DurationInMinutes { get; set; }
        public bool IsActive { get; set; }
        public double SuccessRate { get; set; }
        public int QuestionsNumber { get; set; }
        public double TotalScore { get; set; }
    }
}
