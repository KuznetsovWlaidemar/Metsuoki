using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Application.MediatR.Products.Queries.GetAllProductsQuery;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Products.Queries.GetProductByIdQuery;
public record GetProductByIdQuery(Guid Id) : IRequest<IResult>;
public class GetProductByIdQueryHandler(
    IMetsuokiDbContext context,
    ILogger<GetProductByIdQueryHandler> logger
) : IRequestHandler<GetProductByIdQuery, IResult>
{
    public async Task<IResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Designer)
                .Include(p => p.Images)
                .Include(p => p.Discounts)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                return Result<ProductDto>.NotFound($"Товар не найден.");
            }

            var dto = Mapper.ToDto(product);
            return Result<ProductDto>.Ok(dto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении товара по ID: {Id}", request.Id);
            return Result<string>.BadRequest("Произошла ошибка при получении товара.");
        }
    }
}
