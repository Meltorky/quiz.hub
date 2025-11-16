using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Infrastructure.Data;

namespace quiz.hub.Infrastructure.Repositories.Comman
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAnswerRepo Answers { get; private set; }
        public ICandidateRepo Candidates { get; private set; }
        public IHostRepo Hosts { get; private set; }
        public IQuizRepo Quizzes { get; private set; }
        public IQuestionRepo Questions { get; private set; }

        public UnitOfWork(
            AppDbContext context, 
            IAnswerRepo answerRepo, 
            ICandidateRepo candidateRepo, 
            IHostRepo hostRepo, 
            IQuizRepo quizRepo, 
            IQuestionRepo questionRepo)
        {
            _context = context;
            Answers = answerRepo;
            Candidates = candidateRepo;
            Hosts = hostRepo;
            Quizzes = quizRepo;
            Questions = questionRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
