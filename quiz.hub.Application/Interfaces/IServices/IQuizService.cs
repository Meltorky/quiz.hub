using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface IQuizService
    {
        Task<CreateQuizResultDTO> CreateQuiz(string userId, CreateQuizDTO dto, CancellationToken token);
        Task<QuizDTO> PublishQuiz(CreateQuizResultDTO dto, CancellationToken token);
        Task<bool> ActivateQuiz(CreateQuizResultDTO dto, CancellationToken token);
        Task<bool> DeactivateQuiz(CreateQuizResultDTO dto, CancellationToken token);
        Task RemoveQuiz(CreateQuizResultDTO dto, CancellationToken token);
        Task EditQuiz(EditQuizDTO dto, CancellationToken token);
        Task<double> CalcAvgScore(Guid quizId, CancellationToken token);
        Task<List<QuizDTO>> GetAll(string userId, string Position, int pageNumber, int pageSize, CancellationToken token);    }
}
