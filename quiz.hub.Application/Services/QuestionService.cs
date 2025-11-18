using Microsoft.EntityFrameworkCore;
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
            // Validate at least 1 answer should be correct
            if (!dto.answerDTOs.Any(a => a.IsCorrect))
                throw new ValidationException("At least one answer must be marked as correct.");

            // Validate quiz exists
            var quiz = await _unitOfWork.Quizzes.FindById(dto.QuizId, token)
                ?? throw new NotFoundException($"Quiz with ID '{dto.QuizId}' was not found.");

            // Handle image if provided
            byte[]? image = dto.Image is not null ? await dto.Image.HandleImage() : null;

            // Add question (still no commit)
            var createdQuestion = await _unitOfWork.Questions.AddAsync(dto.ToQuestionEntity(image), token);

            // create answer entities
            var answerEntities = dto.answerDTOs.ToAnswerEntities(createdQuestion.Id);

            // Add answers (still no commit)
            await _unitOfWork.Answers.AddRangeAsync(answerEntities, token);

            // add question score to quiz
            quiz.TotalScore += createdQuestion.Score;

            // commit all
            await _unitOfWork.SaveChangesAsync(token);

            // Map to DTO without reloading from DB (best practice)
            return createdQuestion.ToDTO(answerEntities);
        }


        // edit question
        public async Task<QuestionDTO> EditQuestion(EditQuestionDTO dto, CancellationToken token)
        {
            var question = await _unitOfWork.Questions.FindById(dto.QuestionId, token)
                ?? throw new NotFoundException("Invalid Question ID !!"); ;

            // Validate quiz exists
            var quiz = await _unitOfWork.Quizzes.FindById(dto.QuizId, token)
                ?? throw new NotFoundException($"Quiz with ID '{dto.QuizId}' was not found.");

            quiz.TotalScore = quiz.TotalScore - question.Score + dto.Score;

            question.Score = dto.Score;
            question.Title = dto.Title;

            if (dto.Image is not null)
                question.Image = await dto.Image.HandleImage();

            await _unitOfWork.Questions.Edit(question, token);
            await _unitOfWork.SaveChangesAsync(token);

            return question.ToQuestionDTO();
        }


        // remove question
        public async Task RemoveQuestionWithAnsers(Guid questionId, CancellationToken token)
        {
            var question = await _unitOfWork.Questions.FindById(questionId, token, q => q.Include(q => q.Answers))
                ?? throw new NotFoundException($"Question with ID: '{questionId}' doesn't exist !!");

            var quiz = await _unitOfWork.Quizzes.FindById(question.QuizId, token)
                ?? throw new NotFoundException($"Quiz with ID '{question.QuizId}' was not found.");

            quiz.TotalScore -= question.Score;

            await _unitOfWork.Answers.DeleteRangeAsync(question.Answers, token);
            await _unitOfWork.Questions.DeleteAsync(question, token);
            await _unitOfWork.SaveChangesAsync(token);
        }



        //// create question
        //public async Task<QuestionDTO> CreateQuestion(CreateQuestionDTO dto, CancellationToken token)
        //{
        //    if (await _unitOfWork.Quizzes.FindById(dto.QuizId, token) is null)
        //        throw new NotFoundException($"Quiz with ID '{dto.QuizId}' was not found.");

        //    byte[]? image = null;
        //    if (dto.Image is not null)
        //        image = await dto.Image.HandleImage();

        //    var question = await _unitOfWork.Questions.AddAsync(dto.ToQuestionEntity(image), token);

        //    return question.ToQuestionDTO();
        //}


        //// edit question
        //public async Task<QuestionWithAnswersDTO> EditQuetionWithAnsers(EditQuestionWithAnswersDTO dto, CancellationToken token)
        //{
        //    var question = await _unitOfWork.Questions
        //        .FindUnique(q => q.Id == dto.questionDTO.QuestionId && q.QuizId == dto.questionDTO.QuizId, token);

        //    if (question is null)
        //        throw new NotFoundException("Question does not exist in this quiz.");

        //    question.Score = dto.questionDTO.Score;
        //    question.Title = dto.questionDTO.Title;

        //    if (dto.questionDTO.Image is not null)
        //        question.Image = await dto.questionDTO.Image.HandleImage();

        //    var existingAnswers = await _unitOfWork.Answers.GetRange(question.Id, token);

        //    foreach (var dtoAnswer in dto.answerDTOs)
        //    {
        //        var answer = existingAnswers.Single(a => a.Id == dtoAnswer.Id);

        //        answer.Text = dtoAnswer.Text;
        //        answer.IsCorrect = dtoAnswer.IsCorrect;
        //    }

        //    var newAnswersIds = dto.answerDTOs.Select(a => a.Id).Except(existingAnswers.Select(a => a.Id)).ToList();

        //    var newAnswerEntities = dto.answerDTOs
        //        .Where(x => newAnswersIds.Contains(x.Id))
        //        .ToList()
        //        .ToAnswerEntities(dto.questionDTO.QuestionId);

        //    await _unitOfWork.Answers.AddRangeAsync(newAnswerEntities, token);

        //    if (!dto.answerDTOs.Any(a => a.IsCorrect))
        //        throw new ValidationException("At least one answer must be marked as correct.");

        //    await _unitOfWork.Questions.Edit(question,token);
        //    await _unitOfWork.Answers.EditRange(existingAnswers, token);

        //    await _unitOfWork.SaveChangesAsync(token);

        //    return question.ToDTO(existingAnswers);
        //}

    }
}
