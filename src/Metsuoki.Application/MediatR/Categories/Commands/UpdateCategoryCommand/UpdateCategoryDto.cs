namespace Metsuoki.Application.MediatR.Categories.Commands.UpdateCategoryCommand;
public class UpdateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}