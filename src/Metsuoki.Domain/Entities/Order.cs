using Metsuoki.Domain.Common;
using Metsuoki.Domain.Enums;
using Metsuoki.Domain.Identity;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Заказ
/// </summary>
public class Order : AuditableEntity
{
    /// <summary>
    /// Id клиента
    /// </summary>
    public Guid CustomerId { get; set; }
    public User Customer { get; set; }

    /// <summary>
    /// Позиции заказа
    /// </summary>
    public List<OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Общая цена заказа
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}