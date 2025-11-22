using quiz.hub.Application.DTOs.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface ICandidateAnswerService
    {
        Task<List<AnswerDTO>> GetAllByCandidateID(Guid Id, CancellationToken token);
    }
}
