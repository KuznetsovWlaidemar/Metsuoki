namespace Metsuoki.Application.MediatR.Categories.Commands.CreateCategoryCommand;
public class CreateCategoryDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}