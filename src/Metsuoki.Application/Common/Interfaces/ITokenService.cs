using System.Security.Claims;
using Metsuoki.Domain.Identity;

namespace Metsuoki.Application.Common.Interfaces;

/// <summary>
/// Интерфейс описывающий методы для генерации токенов
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерирует JWT-токен для указанного пользователя.
    /// </summary>
    /// <param name="user">Пользователь, для которого создаётся токен.</param>
    /// <returns>Строка, содержащая сгенерированный JWT-токен.</returns>
    string GenerateJwtToken(User user, List<Claim> claims);

    /// <summary>
    /// Генерирует refresh token
    /// </summary>
    /// <returns>Строка, содержащая сгенерированный refresh token.</returns>
    string GenerateRefreshToken();
}