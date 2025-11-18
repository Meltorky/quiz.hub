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
    }
}
