using Metsuoki.Domain.Common;
using Metsuoki.Domain.Entities.Products;
using Metsuoki.Domain.Identity;

namespace Metsuoki.Domain.Entities;

/// <summary>
/// Оценка
/// </summary>
public class Review : AuditableEntity
{
    /// <summary>
    /// Id оценивающего
    /// </summary>
    public Guid AuthorId { get; set; }

    public User Author { get; set; }

    /// <summary>
    /// Id товара
    /// </summary>
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    /// <summary>
    /// Оценка (1 - 5)
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Описание оценки
    /// </summary>
    public string? Text { get; set; }
}