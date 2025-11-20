using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories.Comman
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepo Answers { get; }
        ICandidateRepo Candidates { get; }
        IHostRepo Hosts { get;  }
        IQuizRepo Quizzes { get;}
        IQuestionRepo Questions { get;}
        IQuizCandidatesRepo QuizCandidates { get; }
        ICommanRepo Commans { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
