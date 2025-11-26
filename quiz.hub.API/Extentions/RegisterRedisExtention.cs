using quiz.hub.Application.Interfaces.IRepositories.Redis;
using quiz.hub.Infrastructure.Repositories.Redis;
using StackExchange.Redis;

namespace quiz.hub.API.Extentions
{
    public static class RegisterRedisExtention
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Redis connection
            var redisConnection = configuration.GetValue<string>("Redis:Connection") ?? "localhost:6379";
            var mux = ConnectionMultiplexer.Connect(redisConnection);
            services.AddSingleton<IConnectionMultiplexer>(mux);

            // Redis service
            services.AddScoped<IRedisCacheService, RedisCacheService>();

            // Other infrastructure services (EF, identity, etc.)
            return services;
        }
    }
}
