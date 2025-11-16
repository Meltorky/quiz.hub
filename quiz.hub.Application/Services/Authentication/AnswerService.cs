using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Mappers;

namespace quiz.hub.Application.Services.Authentication
{
    public class AnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // create answer
        public async Task<AnswerDTO> CreateAnswer(CreateAnswerDTO dto, CancellationToken token) 
        {
            if(await _unitOfWork.Questions.FindById(dto.QuestionId, token) is null)
                throw new NotFoundException($"Question with ID '{dto.QuestionId}' doesn't exist !!");

            var answer = await _unitOfWork.Answers.AddAsync(dto.ToAnswer(), token);

            return answer.ToAnswerDTO();
        }

        // 

        // remove answer
        public async Task DeleteAnswer(Guid answerId, CancellationToken token)
        {
            var answer = await _unitOfWork.Answers.FindById(answerId, token);

            if (answer is null)
                throw new NotFoundException($"Answer with ID '{answerId}' not found.");

            await _unitOfWork.Answers.DeleteAsync(answer, token);
        }

    }
}
