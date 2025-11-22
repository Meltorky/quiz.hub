using quiz.hub.Application.DTOs.CandidateDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Domain.Comman;

namespace quiz.hub.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CandidateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // get all candidates
        public async Task<List<CandidateDTO>> GetAll(Guid quizId, CancellationToken token) 
        {
            Pagination pagination = new Pagination();
            var candidates = await _unitOfWork.QuizCandidates.GetQuizCandidates(quizId, pagination, token);
            return candidates.Select(c => new CandidateDTO
            {
                QuizId = quizId,
                CandidateId = c.CandidateId,
                AttemptedAt = c.AttemptedAt,
                Email = c.Candidate.Email,
                TotalScore = c.TotalScore,
            }).ToList();
        }
    }
}
