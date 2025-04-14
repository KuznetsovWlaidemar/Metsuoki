using Metsuoki.Domain.Common;

namespace Metsuoki.Domain.Entities.Products;
public class ProductDiscount : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public decimal Percentage { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
}
