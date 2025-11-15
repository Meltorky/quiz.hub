using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories.Comman
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepo AnswerRepo { get; }
        ICandidateRepo CandidateRepo { get; }
        IHostRepo HostRepo { get; }
        IQuizRepo QuizRepo { get; }
        IQuestionRepo QuestionRepo { get; }

        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
