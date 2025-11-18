using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Infrastructure.Data;

namespace quiz.hub.Infrastructure.Repositories
{
    public class QuizCandidatesRepo(AppDbContext _context) : IQuizCandidatesRepo
    {
        public async Task<double> CalcAvgScore(Guid quizId, CancellationToken token)
        {
            return await _context.QuizCandidates
                .Where(c => c.QuizId == quizId)
                .Select(c => (double?)c.TotalScore)
                .AverageAsync(token) ?? 0;
        }

    }
}
