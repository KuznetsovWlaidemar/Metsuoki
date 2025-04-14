using Metsuoki.Domain.Common;
using Metsuoki.Domain.Identity;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Корзина
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Id клиента
    /// </summary>
    public Guid UserId { get; set; }
    public User User { get; set; }

    /// <summary>
    /// Список позиций в корзине
    /// </summary>
    public List<CartItem> CartItems { get; set; }
}