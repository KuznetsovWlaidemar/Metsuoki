using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products;
public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string Color { get; set; }
    public string Size { get; set; }

    public int Stock { get; set; } // Сколько на складе

    public decimal? PriceOverride { get; set; } // Персональная цена (если отличается)
}