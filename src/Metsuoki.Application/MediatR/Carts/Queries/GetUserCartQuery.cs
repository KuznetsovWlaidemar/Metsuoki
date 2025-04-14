using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Carts.Queries;
public record GetUserCartQuery(Guid UserId) : IRequest<IResult>;
public class GetUserCartQueryHandler(
    IMetsuokiDbContext context,
    ILogger<GetUserCartQueryHandler> logger
) : IRequestHandler<GetUserCartQuery, IResult>
{
    public async Task<IResult> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null)
                return Result<string>.NotFound("Корзина не найдена");

            var dto = Mapper.ToDto(cart);
            return Result<CartDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении корзины");
            return Result<string>.BadRequest("Произошла ошибка при получении корзины.");
        }
    }
}

