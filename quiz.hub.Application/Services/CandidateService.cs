using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices;

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

        // start quiz => view questions/answers/   : bG task to send quiz from after Dura 

    }
}
