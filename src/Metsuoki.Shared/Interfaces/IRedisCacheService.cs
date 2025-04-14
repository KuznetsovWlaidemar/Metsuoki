namespace Metsuoki.Shared.Interfaces;
public interface IRedisCacheService
{
    /// <summary>
    /// Сохраняет значение в Redis с указанным временем жизни.
    /// </summary>
    /// <param name="key">Ключ для хранения значения.</param>
    /// <param name="value">Значение, которое нужно сохранить.</param>
    /// <param name="expiration">Время жизни для сохранённого значения.</param>
    Task SetAsync(string key, string value, TimeSpan expiration);

    /// <summary>
    /// Получает значение из Redis по ключу.
    /// </summary>
    /// <param name="key">Ключ, по которому нужно получить значение.</param>
    /// <returns>Значение, сохранённое по ключу, или <c>default(T)</c>, если значение не найдено.</returns>
    Task<T> GetAsync<T>(string key);

    /// <summary>
    /// Проверяет, существует ли ключ в Redis.
    /// </summary>
    /// <param name="key">Ключ, который нужно проверить на существование.</param>
    /// <returns><c>true</c>, если ключ существует, иначе <c>false</c>.</returns>
    Task<bool> KeyExistsAsync(string key);

    /// <summary>
    /// Удаляет значение по ключу из Redis.
    /// </summary>
    /// <param name="key">Ключ, по которому нужно удалить значение.</param>
    Task RemoveAsync(string key);
}

