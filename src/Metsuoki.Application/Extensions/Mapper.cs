using Metsuoki.Application.MediatR.Carts.Queries;
using Metsuoki.Application.MediatR.Categories.Queries.GetAllCategoriesQuery;
using Metsuoki.Application.MediatR.Order.Queries;
using Metsuoki.Application.MediatR.Products.Queries.GetAllProductsQuery;
using Metsuoki.Domain.Entities;
using Metsuoki.Domain.Entities.Products;

namespace Metsuoki.Application.Extensions;
public static class Mapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            CurrentPrice = product.CurrentPrice,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            DesignerId = product.DesignerId,
            DesignerName = product.Designer?.FullName,
            ImageUrls = product.Images?.Select(img => img.Url).ToList() ?? new(),
            Discounts = product.Discounts?.Select(d => new ProductDiscountDto
            {
                ProductId = d.ProductId,
                Percentage = d.Percentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
            }).ToList() ?? new(),
            AverageRating = product.Reviews?.Any() == true
                ? Math.Round(product.Reviews.Average(r => r.Rating), 2)
                : 0.0
        };
    }

    public static List<ProductDto> ToDtoList(List<Product> products)
    {
        return products.Select(ToDto).ToList();
    }

    public static OrderDto ToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CustomerName = order.Customer?.FullName,
            TotalAmount = order.TotalAmount,
            Status = order.Status.ToString(),
            Items = order.OrderItems?.Select(oi => new OrderItemDto
            {
                ProductId = oi.ProductId,
                ProductTitle = oi.Product?.Title
            }).ToList() ?? new()
        };
    }

    public static List<OrderDto> ToDtoList(List<Order> orders)
    {
        return orders.Select(ToDto).ToList();
    }
    public static CartDto ToDto(Cart cart)
    {
        return new CartDto
        {
            UserId = cart.UserId,
            Items = cart.CartItems?.Select(ci => new CartItemDto
            {
                ProductId = ci.ProductId,
                ProductTitle = ci.Product?.Title ?? string.Empty,
                Quantity = ci.Quantity
            }).ToList() ?? new List<CartItemDto>()
        };
    }

    public static List<CartDto> ToDtoList(List<Cart> carts)
    {
        return carts.Select(ToDto).ToList();
    }

    public static CategoryDto ToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            ParentCategoryName = category.ParentCategory?.Name,
            Subcategories = category.Subcategories?.Select(sc => new SubcategoryDto
            {
                Id = sc.Id,
                Name = sc.Name
            }).ToList() ?? new()
        };
    }

    public static List<CategoryDto> ToDtoList(List<Category> categories)
    {
        return categories.Select(ToDto).ToList();
    }
}

