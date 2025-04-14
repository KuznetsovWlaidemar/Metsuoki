using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Категория
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Наименование категории
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание категории
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Внешний ключ для родительской категории (если есть)
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Родительская категория
    /// </summary>
    public Category? ParentCategory { get; set; }

    /// <summary>
    /// Дочерние категории
    /// </summary>
    public List<Category> Subcategories { get; set; }

    public List<Product> Products { get; set; }
}