using quiz.hub.Application.DTOs.AnswerDTOs;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Mappers
{
    public static class AnswerMappers
    {
        public static List<Answer> ToAnswerEntites(this List<CreateAnswerDTO> dtos, Guid questionId)
        {
            List<Answer> answers = new List<Answer>();
            foreach (var answerDTO in dtos)
                answers.Add(new Answer
                {
                    Text = answerDTO.Text,
                    IsCorrect = answerDTO.IsTrue,
                    QuestionId = questionId,
                });
            return answers;
        }
    }
}
