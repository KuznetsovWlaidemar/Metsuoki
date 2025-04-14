using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Позиция заказа
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    /// <summary>
    /// Id заказа
    /// </summary>
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}