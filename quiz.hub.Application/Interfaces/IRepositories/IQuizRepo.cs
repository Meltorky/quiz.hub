using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Comman;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories
{
    public interface IQuizRepo : IBaseRepository<Quiz>
    {
        Task<JoinQuizResultDTO?> ConnectQuiz(JoinQuizDTO dto, CancellationToken token);
    }
}
