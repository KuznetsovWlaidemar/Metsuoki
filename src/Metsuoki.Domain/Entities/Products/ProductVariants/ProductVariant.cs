using System.ComponentModel.DataAnnotations.Schema;
using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products.ProductVariants;

/// <summary>
/// Варианты товара
/// </summary>
public class ProductVariant : BaseEntity
{
    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Цвет товара
    /// </summary>
    public Guid ColorId { get; set; }

    public ColorReference Color { get; set; }

    /// <summary>
    /// Размер товара
    /// </summary>
    public Guid SizeId { get; set; }

    public SizeReference Size { get; set; }

    /// <summary>
    /// Сколько на складе
    /// </summary>
    public int Stock { get; set; }

    /// <summary>
    /// Персональная цена (если отличается от базовой)
    /// </summary>
    public decimal? PriceOverride { get; set; }

    [NotMapped]
    public decimal ActualPrice => PriceOverride ?? Product.BasePrice;

    //public decimal CurrentPrice
    //{
    //    get
    //    {
    //        var basePrice = PriceOverride ?? Product.BasePrice;
    //        var discount = Product.Discounts.FirstOrDefault(d => d.IsActive);
    //        return discount != null
    //            ? Math.Round(basePrice * (1 - discount.Percentage / 100), 2)
    //            : basePrice;
    //    }
    //}
}