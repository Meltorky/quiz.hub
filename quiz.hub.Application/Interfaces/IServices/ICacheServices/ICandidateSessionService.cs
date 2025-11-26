using quiz.hub.Application.DTOs.CacheDTOs;
using quiz.hub.Application.DTOs.CasheDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices.ICacheServices
{
    public interface ICandidateSessionService
    {
        Task SaveAnswerAsync(SaveAnswerDTO request);
        Task<Dictionary<Guid, Guid>> GetAnswersAsync(Guid candidateId, Guid quizId);
        Task ClearSessionAsync(Guid candidateId, Guid quizId);
        Task<bool> SubmitQuizAsync(Guid candidateId, Guid quizId, CancellationToken ct = default);
    }

}
