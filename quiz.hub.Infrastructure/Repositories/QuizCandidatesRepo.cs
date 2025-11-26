using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Domain.Comman;
using quiz.hub.Domain.Entities;
using quiz.hub.Infrastructure.Data;
using quiz.hub.Infrastructure.Repositories.Comman;

namespace quiz.hub.Infrastructure.Repositories
{
    public class QuizCandidatesRepo : BaseRepository<QuizCandidate> , IQuizCandidatesRepo
    {
        private readonly AppDbContext _context;
        public QuizCandidatesRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<double> CalcAvgScore(Guid quizId, CancellationToken token)
        {
            return await _context.QuizCandidates
                .Where(c => c.QuizId == quizId)
                .Select(c => (double?)c.TotalScore)
                .AverageAsync(token) ?? 0;
        }

        public async Task<List<Quiz>> GetCandidateQuizzes(string userId, Pagination pagination,CancellationToken token) 
        {
            return await _context.QuizCandidates
                .Where(c => c.CandidateUserId == userId)
                .Select(x => x.Quiz)
                .Skip(pagination.Skip)
                .Take(pagination.Take)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<List<QuizCandidate>> GetQuizCandidates(Guid quizId, Pagination pagination, CancellationToken token)
        {
            return  await _context.QuizCandidates
                .Where(c => c.QuizId == quizId)
                .Include(x => x.Candidate)
                .Skip(pagination.Skip)
                .Take(pagination.Take)
                .AsNoTracking()
                .ToListAsync(token);
        }

    }
}
