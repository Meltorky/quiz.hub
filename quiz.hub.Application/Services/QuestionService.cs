using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.QuestionDTOs;
using quiz.hub.Application.Helpers;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Mappers;

namespace quiz.hub.Application.Services
{
    public class QuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // create question
        public async Task<Guid> CreatQuestion(CreateQuestionDTO dto, CancellationToken token) 
        {
            if (await _unitOfWork.Quizzes.FindById(dto.QuizId, token) is null)
                throw new NotFoundException($"No Quiz exist with id {dto.QuizId}");

            byte[]? image = null;
            if (dto.Image is not null)
                image = await dto.Image.HandleImage();

            var createdQuestion = await _unitOfWork.Questions.AddAsync(dto.ToQuestionEntity(image) , token);

            await _unitOfWork.Answers.AddRangeAsync(dto.answerDTOs.ToAnswerEntites(createdQuestion.Id), token);

            return createdQuestion.Id;
        }
    }
}
