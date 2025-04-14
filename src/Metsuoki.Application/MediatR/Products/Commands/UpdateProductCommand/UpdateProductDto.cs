namespace Metsuoki.Application.MediatR.Products.Commands.UpdateProductCommand;
public class UpdateProductDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal CurrentPrice { get; set; }
    public Guid CategoryId { get; set; }
    public Guid DesignerId { get; set; }
}