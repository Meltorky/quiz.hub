using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Application.Mappers;

namespace quiz.hub.Application.Services.Authentication
{
    public class AnswerService : IAnswerService
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
            await _unitOfWork.SaveChangesAsync(token);

            return answer.ToAnswerDTO();
        }


        // Edit answer
        public async Task<AnswerDTO> EditAnswer(AnswerDTO dto, CancellationToken token)
        {
            var answer = await _unitOfWork.Answers.FindById(dto.Id, token)
                ?? throw new NotFoundException($"Answer with ID '{dto.Id}' doesn't exist !!");

            int trueCount = await _unitOfWork.Answers.NumberOfTrueAnswers(dto.QuestionId, token);

            if ( trueCount == 1 && answer.IsCorrect == true && dto.IsCorrect == false)
                throw new ValidationException("At least one answer must be correct.");

            answer.Text = dto.Text;
            answer.IsCorrect = dto.IsCorrect;

            await _unitOfWork.Answers.Edit(answer,token);

            return dto;
        }


        // remove answer
        public async Task DeleteAnswer(Guid answerId, CancellationToken token)
        {
            var answer = await _unitOfWork.Answers.FindById(answerId, token) 
                ?? throw new NotFoundException($"Answer with ID '{answerId}' not found.");

            int trueCount = await _unitOfWork.Answers.NumberOfTrueAnswers(answer.QuestionId, token);

            if (trueCount == 1 && answer.IsCorrect == true)
                throw new ValidationException("Cannot delete the only correct answer.");

            await _unitOfWork.Answers.DeleteAsync(answer!, token);
        }

    }
}
