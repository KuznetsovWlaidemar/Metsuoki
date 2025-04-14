namespace Metsuoki.Application.MediatR.Tokens.Dtos;

/// <summary>
/// DTO, содержащий информацию о токенах аутентификации.
/// </summary>
public class TokenDto
{
    /// <summary>
    /// Токен доступа (Auth Token), используемый для аутентификации пользователя.
    /// </summary>
    public string AuthToken { get; set; }

    /// <summary>
    /// Токен обновления (Refresh Token), используемый для получения нового токена доступа.
    /// </summary>
    public string RefreshToken { get; set; }
}