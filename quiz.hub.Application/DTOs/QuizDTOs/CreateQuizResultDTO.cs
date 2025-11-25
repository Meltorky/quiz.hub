using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.QuizDTOs
{
    public class CreateQuizResultDTO
    {
        public Guid QuizId { get; set; }
        public string HostId { get; set; } = default!;
    }
}
