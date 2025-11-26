using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Domain.Entities;

namespace quiz.hub.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CandidateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // join Quiz
        public async Task<JoinQuizResultDTO> JoinQuiz(JoinQuizDTO dto, CancellationToken token) 
        {
            var quiz = await _unitOfWork.Quizzes.ConnectQuiz(dto, token) 
                ?? throw new NotFoundException("Invalid Connection Code");

            return quiz;
        }

        public async Task JoinQuiz(string candidateId, Guid quizId,CancellationToken token)
        {
            var result = await _unitOfWork.QuizCandidates.AddAsync(new QuizCandidate 
            {
                AttemptedAt = DateTime.UtcNow,
                CandidateUserId = candidateId,
                TotalScore = 0,
                QuizId = quizId,
            },token);

            await _unitOfWork.SaveChangesAsync(token);
        }

        // start quiz => view questions/answers/   : bG task to send quiz from after Dura 

    }
}
