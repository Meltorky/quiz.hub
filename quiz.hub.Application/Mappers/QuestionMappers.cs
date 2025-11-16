using quiz.hub.Application.DTOs.QuestionDTOs;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace quiz.hub.Application.Mappers
{
    public static class QuestionMappers
    {
        public static Question ToQuestionEntity(this CreateQuestionDTO dto, byte[]? image)
        {
            return new Question
            {
                QuizId = dto.QuizId,
                Title = dto.Title,
                Score = dto.Score,
                Image = image,
                Order = dto.Order
            };
        }

        public static QuestionWithAnswersDTO ToDTO(this Question question, List<Answer> answers)
        {
            return new QuestionWithAnswersDTO
            {
                QuizId = question.QuizId,
                QuestionId = question.Id,
                Title = question.Title,
                Score = question.Score,
                Image = question.Image,
                Order = question.Order,
                answers = answers.ToAnswerDTOs()
            };
        }
    }
}
