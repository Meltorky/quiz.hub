using quiz.hub.Domain.Comman;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories
{
    public interface IQuizCandidatesRepo
    {
        Task<double> CalcAvgScore(Guid quizId, CancellationToken token);
        Task<List<Quiz>> GetCandidateQuizzes(Guid QuizId, Pagination pagination, CancellationToken token);
        Task<List<QuizCandidate>> GetQuizCandidates(Guid quizId, Pagination pagination, CancellationToken token);
    }
}
