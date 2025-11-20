using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Comman;

namespace quiz.hub.Application.Services
{
    public class CandidateAnswerServive
    {
        private readonly IUnitOfWork _unitOfWork;
        public CandidateAnswerServive(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // get candidate answers
        public async Task<List<AnswerDTO>> GetAllByCandidateID(Guid Id, CancellationToken token)
        {
            Pagination pagination = new Pagination();
            var list = await _unitOfWork.CandidateAnswers
                .GetAll(pagination,token,x => x.Where(ca => ca.CandidateId == Id));

            return list.Select(i => new AnswerDTO 
            {
                Id = i.AnswerId,
                QuestionId = i.QuestionId,
                
            }).ToList();
        }
    }
}
