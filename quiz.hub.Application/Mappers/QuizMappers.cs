using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Mappers
{
    public static class QuizMappers
    {
        public static QuizDTO ToQuizDTO(this Quiz quiz)
        {
            return new QuizDTO 
            {
                Id = quiz.Id,
                QuestionsNumber = quiz.QuestionsNumber,
                ConnectionCode = quiz.ConnectionCode,
                CreatedAt = quiz.CreatedAt,
                Description = quiz.Description,
                DurationInMinutes = quiz.DurationInMinutes,
                HostId = quiz.HostId,
                IsActive = quiz.IsActive,
                IsPublished = quiz.IsPublished,
                PublishedAt = quiz.PublishedAt,
                SuccessRate = quiz.AverageScore,
                Title = quiz.Title,
                TotalScore = quiz.TotalScore
            };
        } 
    }
}
