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
        public async Task<QuestionWithAnswersDTO> CreatQuestionWithAnswers(CreateQuestionDTO dto, CancellationToken token)
        {
            // Validate quiz exists
            if (await _unitOfWork.Quizzes.FindById(dto.QuizId, token) is null)
                throw new NotFoundException($"Quiz with ID '{dto.QuizId}' was not found.");

            // Handle image if provided
            byte[]? image = null;
            if (dto.Image is not null)
                image = await dto.Image.HandleImage();

            // Add question (still no commit)
            var createdQuestion = await _unitOfWork.Questions.AddAsync(dto.ToQuestionEntity(image), token);

            // create answer entities
            var answerEntities = dto.answerDTOs.ToAnswerEntities(createdQuestion.Id);

            // Add answers (still no commit)
            await _unitOfWork.Answers.AddRangeAsync(answerEntities, token);

            // commit all
            await _unitOfWork.SaveChangesAsync(token);

            // Map to DTO without reloading from DB (best practice)
            return createdQuestion.ToDTO(answerEntities);
        }


        // edit question
        public async Task<QuestionWithAnswersDTO> EditQuetion(EditQuestionWithAnswersDTO dto, CancellationToken token)
        {
            var question = await _unitOfWork.Questions
                .FindUnique(q => q.Id == dto.questionDTO.QuestionId && q.QuizId == dto.questionDTO.QuizId, token);

            if (question is null)
                throw new NotFoundException("Question does not exist in this quiz.");

            question.Score = dto.questionDTO.Score;
            question.Title = dto.questionDTO.Title;

            if (dto.questionDTO.Image is not null)
                question.Image = await dto.questionDTO.Image.HandleImage();

            var existingAnswers = await _unitOfWork.Answers.GetRange(question.Id, token);

            foreach (var dtoAnswer in dto.answerDTOs)
            {
                var answer = existingAnswers.Single(a => a.Id == dtoAnswer.Id);

                answer.Text = dtoAnswer.Text;
                answer.IsCorrect = dtoAnswer.IsCorrect;
            }

            _unitOfWork.Questions.Edit(question);
            _unitOfWork.Answers.EditRange(existingAnswers);

            await _unitOfWork.SaveChangesAsync(token);

            return question.ToDTO(existingAnswers);
        }
    }
}
