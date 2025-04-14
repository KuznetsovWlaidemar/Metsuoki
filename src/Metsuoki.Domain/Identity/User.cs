using Metsuoki.Domain.Entities;
using Metsuoki.Domain.Entities.Products;
using Microsoft.AspNetCore.Identity;

namespace Metsuoki.Domain.Identity;
/// <summary>
/// Пользователь приложения
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Полное имя пользователя
    /// </summary>
    public string FullName => string.IsNullOrWhiteSpace(Patronymic)
        ? $"{LastName} {FirstName}"
        : $"{LastName} {FirstName} {Patronymic}";

    public string? ShippingAddress { get; set; }

    public string? ContactInfo { get; set; }

    /// <summary>
    /// Дата и время последнего входа пользователя в систему
    /// </summary>
    public DateTime? LastLoginDate { get; set; }

    public List<Product> Products { get; set; } // если он дизайнер
    public List<Order> Orders { get; set; } // если покупатель
    public List<Review> Reviews { get; set; }
    public List<CartItem> CartItems { get; set; } // корзина
}