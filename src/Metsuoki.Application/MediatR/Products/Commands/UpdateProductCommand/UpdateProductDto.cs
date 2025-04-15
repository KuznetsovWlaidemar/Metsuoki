namespace Metsuoki.Application.MediatR.Products.Commands.UpdateProductCommand;
public class UpdateProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal BasePrice { get; set; }
    public Guid CategoryId { get; set; }
    public Guid DesignerId { get; set; }
}