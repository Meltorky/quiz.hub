using quiz.hub.Application.Interfaces.IRepositories;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Infrastructure.Data;

namespace quiz.hub.Infrastructure.Repositories.Comman
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IAnswerRepo Answers { get; private set; }
        public IQuizRepo Quizzes { get; private set; }
        public IQuestionRepo Questions { get; private set; }
        public IQuizCandidatesRepo QuizCandidates { get; private set; }
        public ICandidateAnswerRepo CandidateAnswers { get; private set; }
        public ICommanRepo Commans { get; private set; }

        public UnitOfWork(
            AppDbContext context,
            IAnswerRepo answerRepo,
            IQuizRepo quizRepo,
            IQuestionRepo questionRepo,
            IQuizCandidatesRepo quizCandidates,
            ICommanRepo commans,
            ICandidateAnswerRepo candidateAnswers)
        {
            _context = context;
            Answers = answerRepo;
            Quizzes = quizRepo;
            Questions = questionRepo;
            QuizCandidates = quizCandidates;
            Commans = commans;
            CandidateAnswers = candidateAnswers;
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
