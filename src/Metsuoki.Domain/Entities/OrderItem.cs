using System.ComponentModel.DataAnnotations.Schema;
using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;
using Metsuoki.Domain.Entities.Products.ProductVariants;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Позиция заказа
/// </summary>
public class OrderItem : BaseEntity
{
    /// <summary>
    /// Id заказа
    /// </summary>
    public Guid OrderId { get; set; }

    public Order Order { get; set; }

    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Вариант товара
    /// </summary>
    public Guid ProductVariantId { get; set; }

    public ProductVariant ProductVariant { get; set; }

    /// <summary>
    /// Количество товара
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена за единицу товара в момент заказа
    /// </summary>
    public decimal UnitPrice { get; set; }

    [NotMapped]
    public decimal TotalPrice => Quantity * UnitPrice;
}