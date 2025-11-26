using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using quiz.hub.Application.DTOs.CacheDTOs;
using quiz.hub.Application.DTOs.CasheDTOs;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IRepositories.Redis;
using quiz.hub.Application.Interfaces.IServices.ICacheServices;
using quiz.hub.Domain.Entities;

namespace quiz.hub.Application.Services.CacheServices
{
    // CandidateSessionService.cs
    public class CandidateSessionService : ICandidateSessionService
    {
        private readonly IRedisCacheService _cache;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CandidateSessionService> _logger;

        public CandidateSessionService(IRedisCacheService cache, IUnitOfWork unitOfWork, ILogger<CandidateSessionService> logger)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private static string SessionKey(Guid candidateId, Guid quizId) =>
            $"quiz:session:{candidateId}:{quizId}";

        public async Task SaveAnswerAsync(SaveAnswerDTO request)
        {
            var key = SessionKey(request.CandidateId, request.QuizId);

            // get existing answers
            var current = await _cache.GetAsync<Dictionary<Guid, Guid>>(key) ?? new Dictionary<Guid, Guid>();

            current[request.QuestionId] = request.AnswerId;

            await _cache.SetAsync(key, current, TimeSpan.FromHours(24));
        }

        public async Task<Dictionary<Guid, Guid>> GetAnswersAsync(Guid candidateId, Guid quizId)
        {
            var key = SessionKey(candidateId, quizId);
            return await _cache.GetAsync<Dictionary<Guid, Guid>>(key) ?? new Dictionary<Guid, Guid>();
        }

        public async Task ClearSessionAsync(Guid candidateId, Guid quizId)
        {
            var key = SessionKey(candidateId, quizId);
            await _cache.RemoveAsync(key);
        }

        public async Task<bool> SubmitQuizAsync(Guid candidateId, Guid quizId, CancellationToken token)
        {
            // 1. Get all answers from Redis
            var answers = await GetAnswersAsync(candidateId, quizId);

            if (!answers.Any()) return false; // nothing to submit

            // 2. Map to DB entities
            var session = new List<CandidateAnswer>();

            foreach (var kv in answers)
            {
                session.Add(new CandidateAnswer
                {
                    CandidateId = candidateId.ToString(),
                    QuizId = quizId,
                    QuestionId = kv.Key,
                    AnswerId = kv.Value
                });
            }

            // 3. Save to DB (Infrastructure)
            await _unitOfWork.CandidateAnswers.AddRangeAsync(session, token);

            await _unitOfWork.SaveChangesAsync(token);

            // 4. Clear Redis session
            await ClearSessionAsync(candidateId, quizId);

            return true;
        }

    }

}

