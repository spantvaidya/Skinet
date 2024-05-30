using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{

    public class ResponseCacheService(IConnectionMultiplexer redis) : IResponseCacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timetoLive)
        {
            if (response == null) return;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialsedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey, serialsedResponse, timetoLive);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            if (cachedResponse.IsNullOrEmpty) return null;
            return cachedResponse;
        }
    }
}