using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Domain.Entities;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Carts.Commands.AddToCartCommand;
public record AddToCartCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest<IResult>;
public class AddToCartCommandHandler(
    IMetsuokiDbContext context,
    ILogger<AddToCartCommandHandler> logger
) : IRequestHandler<AddToCartCommand, IResult>
{
    public async Task<IResult> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.UserId,
                    CartItems = new List<CartItem>()
                };
                await context.Carts.AddAsync(cart, cancellationToken);
            }

            var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result<string>.Ok("Товар добавлен в корзину");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при добавлении товара в корзину.");
            return Result<string>.BadRequest("Ошибка при добавлении товара в корзину.");
        }
    }
}
