using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Domain.Entities;
using quiz.hub.Infrastructure.Data;
using quiz.hub.Infrastructure.Repositories.Comman;

namespace quiz.hub.Infrastructure.Repositories
{
    public class QuizRepo : BaseRepository<Quiz> , IQuizRepo 
    {
        private readonly AppDbContext _context;
        public QuizRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
