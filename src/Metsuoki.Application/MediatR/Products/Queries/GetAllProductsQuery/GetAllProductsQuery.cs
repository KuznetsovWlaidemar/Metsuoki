using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Products.Queries.GetAllProductsQuery;
public record GetAllProductsQuery : IRequest<IResult>;

public class GetAllProductsQueryHandler(
    IMetsuokiDbContext context,
    ILogger<GetAllProductsQueryHandler> logger
) : IRequestHandler<GetAllProductsQuery, IResult>
{
    public async Task<IResult> Handle(GetAllProductsQuery command, CancellationToken cancellationToken)
    {
        try
        {
            var products = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Designer)
                .Include(p => p.Discounts)
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .ToListAsync(cancellationToken);

            if (products == null || !products.Any())
            {
                return Result<string>.NotFound("Товары не найдены.");
            }

            var productDtos = Mapper.ToDtoList(products);

            return Result<List<ProductDto>>.Ok(productDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при извлечении товаров.");
            return Result<string>.BadRequest("Произошла ошибка при извлечении товаров.");
        }
    }
}