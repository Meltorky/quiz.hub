using quiz.hub.Application.DTOs.QuestionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface IQuestionService
    {
        Task<QuestionWithAnswersDTO> CreatQuestionWithAnswers(CreateQuestionDTO dto, CancellationToken token);
        Task<QuestionDTO> EditQuestion(EditQuestionDTO dto, CancellationToken token);
        Task RemoveQuestionWithAnsers(Guid questionId, CancellationToken token);
    }
}
