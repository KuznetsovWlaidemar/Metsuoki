using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Carts.Commands.RemoveFromCartCommand;

public record RemoveFromCartCommand(Guid UserId, Guid ProductId) : IRequest<IResult>;
public class RemoveFromCartCommandHandler(
    IMetsuokiDbContext context,
    ILogger<RemoveFromCartCommandHandler> logger
) : IRequestHandler<RemoveFromCartCommand, IResult>
{
    public async Task<IResult> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null)
                return Result<string>.NotFound("Корзина не найдена.");

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (item == null)
                return Result<string>.NotFound("Товар не найден в корзине.");

            cart.CartItems.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            return Result<string>.Ok("Товар удалён из корзины");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении товара из корзины.");
            return Result<string>.BadRequest("Ошибка при удалении товара из корзины.");
        }
    }
}
