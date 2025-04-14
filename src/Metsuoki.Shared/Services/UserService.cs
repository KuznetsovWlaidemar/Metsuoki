using Metsuoki.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Metsuoki.Shared.Services;

/// <summary>
/// Сервис для получения информации о текущем пользователе.
/// </summary>
public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Получает текущее значение из JWT-токена по ключу.
    /// </summary>
    private string? GetClaimValue(string claimType) => _httpContextAccessor.HttpContext?.User?.FindFirst(claimType)?.Value;

    /// <summary>
    /// Получает текущее значение из JWT-токена и преобразует в Guid
    /// </summary>
    private Guid? GetClaimGuidValue(string claimType)
    {
        var value = GetClaimValue(claimType);
        return Guid.TryParse(value, out var guid) ? guid : null;
    }

    public string? UserName => GetClaimValue("FullName");
    public Guid? UserId => GetClaimGuidValue("UserId");
}