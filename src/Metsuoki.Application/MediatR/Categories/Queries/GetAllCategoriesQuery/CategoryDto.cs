namespace Metsuoki.Application.MediatR.Categories.Queries.GetAllCategoriesQuery;
public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public string? ParentCategoryName { get; set; }
    public List<SubcategoryDto> Subcategories { get; set; }
}
public class SubcategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}