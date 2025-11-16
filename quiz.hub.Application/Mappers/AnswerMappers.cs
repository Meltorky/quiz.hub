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
        //public static List<Answer> ToAnswerEntites(this List<CreateAnswerDTO> dtos, Guid questionId)
        //{
        //    List<Answer> answers = new List<Answer>();
        //    foreach (var answerDTO in dtos)
        //        answers.Add(new Answer
        //        {
        //            Text = answerDTO.Text,
        //            IsCorrect = answerDTO.IsTrue,
        //            QuestionId = questionId,
        //        });
        //    return answers;
        //}

        public static List<Answer> ToAnswerEntities(this List<CreateAnswerDTO> dtos, Guid questionId)
        {
            return dtos.Select(dto => new Answer
            {
                Text = dto.Text,
                IsCorrect = dto.IsTrue,
                QuestionId = questionId
            }).ToList();
        }


        public static List<Answer> ToAnswerEntities(this List<AnswerDTO> dtos, Guid questionId)
        {
            return dtos.Select(dto => new Answer
            {
                Id = dto.Id,
                Text = dto.Text,
                IsCorrect = dto.IsCorrect,
                QuestionId = questionId
            }).ToList();
        }


        public static List<AnswerDTO> ToAnswerDTOs(this List<Answer> answers) 
        {
            return answers.Select(answer => new AnswerDTO
            {
                Text = answer.Text,
                Id = answer.Id,
                IsCorrect = answer.IsCorrect
            }).ToList();
        }
    }
}
