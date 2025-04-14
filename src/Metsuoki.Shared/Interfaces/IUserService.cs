namespace Metsuoki.Shared.Interfaces;

/// <summary>
/// Интерфейс для получения информации о текущем пользователе
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Идентификатор текущего пользователя из JWT-токена
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// ФИО пользователя
    /// </summary>
    public string? UserName { get; }
}