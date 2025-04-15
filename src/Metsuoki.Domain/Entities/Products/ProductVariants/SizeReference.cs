using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products.ProductVariants;

/// <summary>
/// Размеры товара
/// </summary>
public class SizeReference : BaseEntity
{
    /// <summary>
    /// Наименование размера
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание размера
    /// </summary>
    public string? Description { get; set; }

    public List<ProductVariant> ProductVariants { get; set; }
}