using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products.ProductVariants;

/// <summary>
/// Цвет товара
/// </summary>
public class ColorReference : BaseEntity
{
    /// <summary>
    /// Наименование цвета
    /// </summary>
    public required string Name { get; set; }
    public string? HexCode { get; set; }

    public List<ProductVariant> ProductVariants { get; set; }
}
