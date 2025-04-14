using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;

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
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }
    /// <summary>
    /// Количестов товара
    /// </summary>
    public int Quantity { get; set; }
}