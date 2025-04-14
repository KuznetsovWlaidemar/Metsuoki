using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products;
/// <summary>
/// Url изображения товара
/// </summary>
public class ProductImage : BaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    /// <summary>
    /// Ссылка на файл в CDN / облаке
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// Порядок отображения
    /// </summary>
    public int Order { get; set; }
}