using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class EditQuizDTO
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public double DurationInMinutes { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
    }
}
