using System.ComponentModel.DataAnnotations.Schema;
using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products.ProductVariants;
using Metsuoki.Domain.Identity;

namespace Metsuoki.Domain.Entities.Products;

/// <summary>
/// Товар
/// </summary>
public class Product : AuditableEntity
{
    /// <summary>
    /// Наименование товара
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Базовая цена товара
    /// </summary>
    public required decimal BasePrice { get; set; }

    /// <summary>
    /// Id категории товара
    /// </summary>
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    /// <summary>
    /// Id дизайнера
    /// </summary>
    public Guid DesignerId { get; set; }
    public User Designer { get; set; }

    /// <summary>
    /// Цена с учётом скидки
    /// </summary>
    [NotMapped]
    public decimal CurrentPrice => Discounts.FirstOrDefault(d => d.IsActive) is { } discount
        ? Math.Round(BasePrice * (1 - discount.Percentage / 100), 2)
        : BasePrice;

    public bool IsVisible { get; set; }
    public List<ProductDiscount> Discounts { get; set; }

    public List<ProductVariant> Variants { get; set; }

    /// <summary>
    /// Ссылка на картинки
    /// </summary>
    public List<ProductImage> Images { get; set; }

    /// <summary>
    /// Список оценок
    /// </summary>
    public List<Review> Reviews { get; set; }
}