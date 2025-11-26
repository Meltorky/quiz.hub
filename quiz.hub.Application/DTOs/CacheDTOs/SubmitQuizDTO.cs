using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.CacheDTOs
{
    public class SubmitQuizDTO
    {
        public Guid QuizId { get; set; }
        public Guid CandidateId { get; set; }
    }

}
