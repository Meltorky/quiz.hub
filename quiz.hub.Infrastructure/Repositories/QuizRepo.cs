using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Domain.Comman;
using quiz.hub.Domain.Entities;
using quiz.hub.Infrastructure.Data;
using quiz.hub.Infrastructure.Repositories.Comman;

namespace quiz.hub.Infrastructure.Repositories
{
    public class QuizRepo : BaseRepository<Quiz>, IQuizRepo
    {
        private readonly AppDbContext _context;
        public QuizRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }


        // Use projection (Select) → Best for performance
        public async Task<JoinQuizResultDTO?> ConnectQuiz(JoinQuizDTO dto, CancellationToken token)
        {
            var quiz = await _context.Quizzes
            .Where(q => q.ConnectionCode == dto.ConnectionCode)
            .Select(q => new JoinQuizResultDTO
            {
                quizId = q.Id,
                CandidateId = dto.CandidateId,
                Description = q.Description,
                Title = q.Title,
                DurationInMinutes = q.DurationInMinutes,
                PublishedAt = q.PublishedAt,
                TotalScore = q.TotalScore,
                IsActive = q.IsActive,
                HostName = q.Host.Name,
                ConnectionCode = q.ConnectionCode,
                NumberOfQuestions = q.Questions.Count(),
                CandiantesEnrolled = q.QuizCandidates.Count()
            })
            .SingleOrDefaultAsync(token);

            return quiz;
        }
    }
}
