using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.QuizDTOs;
using quiz.hub.Application.Helpers;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Application.Interfaces.IServices.Authentication;
using quiz.hub.Application.Mappers;
using quiz.hub.Domain.Comman;
using quiz.hub.Domain.Entities;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;
using System.Data;

namespace quiz.hub.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIsAuthorized _isAuthorized;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IIsAuthorized isAuthorized) : base()
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _isAuthorized = isAuthorized;
        }


        // host create quiz
        public async Task<CreateQuizResultDTO> CreateQuiz(string userId, CreateQuizDTO dto, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId) ??
                throw new NotFoundException($"No user exit ID: {userId} !!");


            Quiz newQuiz = new Quiz
            {
                HostUserId = user.Id,
                Title = dto.Title,
                Description = dto.Description,
                DurationInMinutes = dto.DurationInMinutes,
                ConnectionCode = RandomsHelper.GenerateSecureConnectionCode()
            };

            var result = await _unitOfWork.Quizzes.AddAsync(newQuiz, token);
            await _unitOfWork.SaveChangesAsync(token);

            return new CreateQuizResultDTO
            {
                QuizId = result.Id,
                HostId = result.HostUserId            };
        }


        // publish Quiz
        public async Task<QuizDTO> PublishQuiz(CreateQuizResultDTO dto, CancellationToken token)
        {
            var quiz = await _unitOfWork.Quizzes.FindById(dto.QuizId, token, q => q.Include(i => i.Questions))
              ?? throw new NotFoundException($"Quiz not Found !!");

            if (quiz.HostUserId != dto.HostId)
                throw new ForbiddenAccessException("You are not allowed to Activate this quiz.");

            if (quiz.IsPublished)
                throw new OperationFailedException($"Quiz is Already Published !!");

            if (quiz.Questions.Count < 1)
                throw new OperationFailedException("Quiz should has at least 1 question !!");

            quiz.PublishedAt = DateTime.Now;
            quiz.IsActive = true;
            quiz.IsPublished = true;

            return (await _unitOfWork.Quizzes.Edit(quiz, token)).ToQuizDTO();
        }


        // activate Quiz
        public async Task<bool> ActivateQuiz(CreateQuizResultDTO dto, CancellationToken token)
        {
            var quiz = await _unitOfWork.Quizzes.FindById(dto.QuizId, token, q => q.Include(i => i.Questions))
              ?? throw new NotFoundException($"Quiz not Found !!");

            if (quiz.HostUserId != dto.HostId)
                throw new ForbiddenAccessException("You are not allowed to Activate this quiz.");

            if (!quiz.IsPublished)
                throw new OperationFailedException($"Quiz didnot Published Yet, You should publish the Quiz First !!");

            if (quiz.IsActive)
                throw new OperationFailedException($"Quiz Already Activated !!");

            quiz.IsActive = true;

            return await _unitOfWork.SaveChangesAsync(token) > 0;
        }


        // deActivate Quiz
        public async Task<bool> DeactivateQuiz(CreateQuizResultDTO dto, CancellationToken token)
        {
            var quiz = await _unitOfWork.Quizzes.FindById(dto.QuizId, token, q => q.Include(i => i.Questions))
              ?? throw new NotFoundException($"Quiz not Found !!");

            if (quiz.HostUserId != dto.HostId)
                throw new ForbiddenAccessException("You are not allowed to DeActivate this quiz.");

            if (!quiz.IsPublished)
                throw new OperationFailedException($"Quiz didnot Published Yet, You should publish the Quiz First !!");

            if (!quiz.IsActive)
                throw new OperationFailedException($"Quiz Already is Not Active !!");

            quiz.IsActive = false;

            return await _unitOfWork.SaveChangesAsync(token) > 0;
        }

        // remove Quiz
        public async Task RemoveQuiz(CreateQuizResultDTO dto, CancellationToken token)
        {
            var quiz = await _unitOfWork.Quizzes
                .FindById(
                    dto.QuizId,
                    token,
                    q => q.Include(i => i.Host),
                    q => q.Include(i => i.Questions).ThenInclude(i => i.Answers))
              ?? throw new NotFoundException($"Quiz not Found !!");

            if (quiz.HostUserId != dto.HostId)
                throw new ForbiddenAccessException("You are not allowed to Activate this quiz.");

            if (quiz.IsPublished)
                throw new OperationFailedException($"Can not Remove quiz has already published !!");

            await _unitOfWork.Answers.DeleteRangeAsync(
                quiz.Questions.SelectMany(q => q.Answers).ToList(), token);

            await _unitOfWork.Questions.DeleteRangeAsync(quiz.Questions, token);

            await _unitOfWork.Quizzes.DeleteAsync(quiz,token);  // Cascade, Quiz will bedeleted.
        }


        // Edit Quiz
        public async Task EditQuiz(EditQuizDTO dto, CancellationToken token)
        {
            var quiz = await _unitOfWork.Quizzes.FindById(dto.Id, token)
              ?? throw new NotFoundException($"Quiz not Found !!");

            if (quiz.HostUserId != dto.HostId)
                throw new ForbiddenAccessException("You are not allowed to Activate this quiz.");

            quiz.DurationInMinutes = dto.DurationInMinutes;
            quiz.Title = dto.Title;
            quiz.Description = dto.Description;

            await _unitOfWork.SaveChangesAsync(token);
        }


        // calculate AVG Score
        public async Task<double> CalcAvgScore(Guid quizId, CancellationToken token)
        {
            if (await _unitOfWork.Quizzes.FindById(quizId, token) is null)
                throw new NotFoundException($"Quiz not Found !!");

            return await _unitOfWork.QuizCandidates.CalcAvgScore(quizId, token);
        }


        // get all Quizzes
        public async Task<List<QuizDTO>> GetAll(string userId, string Position, int pageNumber, int pageSize, CancellationToken token)
        {
            Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };

            if (PositionEnums.Admin.ToString().ToLower() == Position.ToLower() && await _isAuthorized.IsAdmin(userId.ToString()))
                return await HandleAdmin(pagination, token);

            else if (PositionEnums.Host.ToString().ToLower() == Position.ToLower())
                return await HandleHost(userId, pagination, token);

            else if (PositionEnums.Candidate.ToString().ToLower() == Position.ToLower())
                return await HandleCandidate(userId, pagination, token);

            else
                throw new ForbiddenAccessException("Access Denied !!");
        }


        private async Task<List<QuizDTO>> HandleAdmin(Pagination pagination, CancellationToken token)
        {
            var quizzes = await _unitOfWork.Quizzes.GetAll(pagination, token);
            return quizzes.ToQuizDTOs();
        }


        private async Task<List<QuizDTO>> HandleHost(string userId, Pagination pagination, CancellationToken token)
        {
            var quizzes = await _unitOfWork.Quizzes.GetAll(pagination, token, h => h.Where(i => i.HostUserId == userId));
            return quizzes.ToQuizDTOs();
        }


        private async Task<List<QuizDTO>> HandleCandidate(string userId, Pagination pagination, CancellationToken token)
        {
            var quizzes = await _unitOfWork.QuizCandidates.GetCandidateQuizzes(userId, pagination, token);
            return quizzes.ToQuizDTOs();
        }





    }
}
