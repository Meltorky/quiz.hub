using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IRepositories.Redis
{
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
