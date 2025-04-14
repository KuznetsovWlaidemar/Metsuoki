namespace Metsuoki.Application.MediatR.Order.Queries;
public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public List<OrderItemDto> Items { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; }
}