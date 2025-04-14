using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Order.Queries;
public record GetAllOrdersQuery : IRequest<IResult>;
public class GetAllOrdersQueryHandler(
    IMetsuokiDbContext context,
    ILogger<GetAllOrdersQueryHandler> logger
) : IRequestHandler<GetAllOrdersQuery, IResult>
{
    public async Task<IResult> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (!orders.Any())
                return Result<string>.NotFound("Заказы не найдены");

            var dtoList = Mapper.ToDtoList(orders);

            return Result<List<OrderDto>>.Ok(dtoList);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказов");
            return Result<string>.BadRequest("Произошла ошибка при извлечении заказов.");
        }
    }
}
