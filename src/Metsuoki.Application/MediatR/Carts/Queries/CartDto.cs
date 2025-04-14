namespace Metsuoki.Application.MediatR.Carts.Queries;
public class CartDto
{
    public Guid UserId { get; set; }
    public List<CartItemDto> Items { get; set; }
}
public class CartItemDto
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
    public int Quantity { get; set; }
}