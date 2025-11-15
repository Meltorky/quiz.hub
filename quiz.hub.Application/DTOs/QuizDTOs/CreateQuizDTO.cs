using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class CreateQuizDTO
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
