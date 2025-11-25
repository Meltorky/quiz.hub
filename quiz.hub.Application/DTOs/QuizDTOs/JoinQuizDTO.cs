using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class JoinQuizDTO
    {
        [Required]
        public string CandidateId { get; set; } = default!;

        [Required]
        public string ConnectionCode { get; set; } = default!;
    }
}
