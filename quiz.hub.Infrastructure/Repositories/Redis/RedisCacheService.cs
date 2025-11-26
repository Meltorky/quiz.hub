using quiz.hub.Application.Interfaces.IRepositories.Redis;
using StackExchange.Redis;
using System.Text.Json;

namespace quiz.hub.Infrastructure.Repositories.Redis
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, ttl);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
