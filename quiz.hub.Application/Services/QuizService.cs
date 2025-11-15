using Microsoft.AspNetCore.Identity;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Helpers;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Entities;
using quiz.hub.Domain.Identity;

namespace quiz.hub.Application.Services
{
    public class QuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        // host create quiz
        public async Task<Guid> CreateQuiz(string userId , CreateQuizDTO dto , CancellationToken token)
        {
            var host = await _unitOfWork.Hosts.FindUnique(h => h.UserId == userId, token);

            if (host is null)
                throw new NotFoundException($"No host exit with user id of {userId} !!");

            Quiz newQuiz = new Quiz
            {
                HostId = host.Id,
                Title = dto.Title,
                Description = dto.Description,
                ConnectionCode = RandomsHelper.GenerateSecureConnectionCode()
            };

            var result = await _unitOfWork.Quizzes.AddAsync(newQuiz , token);
            return result.Id;
        }
    }
}
