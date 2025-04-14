using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Identity;
/// <summary>
/// Refresh-токен
/// </summary>
public class RefreshToken : AuditableEntity
{
    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
    public User User { get; set; }

    /// <summary>
    /// Дата истечения срока действия
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// Отозван
    /// </summary>
    public bool IsRevoked { get; set; }
}