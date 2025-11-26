using quiz.hub.Application.DTOs.CandidateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface ICandidateService
    {
        Task JoinQuiz(string candidateId, Guid quizId, CancellationToken token);
    }
}
