using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;
using Metsuoki.Domain.Entities.Products.ProductVariants;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Позиция в корзине
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Id варианта
    /// </summary>
    public Guid ProductVariantId { get; set; }

    public ProductVariant ProductVariant { get; set; }

    /// <summary>
    /// Id корзины
    /// </summary>
    public Guid CartId { get; set; }

    public Cart Cart { get; set; }

    /// <summary>
    /// Цена на момент добавления в корзину
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Количестов товара
    /// </summary>
    public int Quantity { get; set; }
}