using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories
{
    public interface IAnswerRepo : IBaseRepository<Answer> 
    {
        Task<List<Answer>> GetRange(Guid questionId, CancellationToken token);
    }
}
