namespace Metsuoki.Application.MediatR.Products.Queries.GetAllProductsQuery;

/// <summary>
/// Товар
/// </summary>
public class ProductDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal BasePrice { get; set; }

    public decimal CurrentPrice { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public Guid DesignerId { get; set; }

    public string DesignerName { get; set; }

    public List<string> ImageUrls { get; set; }

    public List<ProductDiscountDto> Discounts { get; set; }

    public double AverageRating { get; set; }
}
public class ProductDiscountDto
{
    public Guid ProductId { get; set; }
    public decimal Percentage { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
}