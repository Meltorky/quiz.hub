using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Infrastructure.Data;

namespace quiz.hub.Infrastructure.Repositories.Comman
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAnswerRepo AnswerRepo { get; private set; }
        public ICandidateRepo CandidateRepo { get; private set; }
        public IHostRepo HostRepo { get; private set; }
        public IQuizRepo QuizRepo { get; private set; }
        public IQuestionRepo QuestionRepo { get; private set; }

        public UnitOfWork(
            AppDbContext context, 
            IAnswerRepo answerRepo, 
            ICandidateRepo candidateRepo, 
            IHostRepo hostRepo, 
            IQuizRepo quizRepo, 
            IQuestionRepo questionRepo)
        {
            _context = context;
            AnswerRepo = answerRepo;
            CandidateRepo = candidateRepo;
            HostRepo = hostRepo;
            QuizRepo = quizRepo;
            QuestionRepo = questionRepo;
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
