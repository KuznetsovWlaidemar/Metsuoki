using Metsuoki.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Metsuoki.Shared.Services;
public class RedisCacheService : IRedisCacheService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisCacheService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RedisConnectionString");
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _database = _redis.GetDatabase();
    }

    public async Task SetAsync(string key, string value, TimeSpan expiration)
    {
        // Сохраняем значение в Redis с указанным временем жизни
        await _database.StringSetAsync(key, value, expiration);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        // Получаем значение из Redis и преобразуем в тип T
        var value = await _database.StringGetAsync(key);
        if (value.IsNullOrEmpty)
        {
            return default(T);
        }

        return JsonConvert.DeserializeObject<T>(value);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        // Проверяем, существует ли ключ в Redis
        return await _database.KeyExistsAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        // Удаляем ключ из Redis
        await _database.KeyDeleteAsync(key);
    }
}
