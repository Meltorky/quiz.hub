using quiz.hub.Application.DTOs.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface IAnswerService
    {
        Task<AnswerDTO> CreateAnswer(CreateAnswerDTO dto, CancellationToken token);
        Task<AnswerDTO> EditAnswer(AnswerDTO dto, CancellationToken token);
        Task DeleteAnswer(Guid answerId, CancellationToken token);
    }
}
