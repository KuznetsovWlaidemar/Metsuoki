using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Categories.Queries.GetAllCategoriesQuery;
public record GetAllCategoriesQuery : IRequest<IResult>;
public class GetAllCategoriesQueryHandler(
    IMetsuokiDbContext context,
    ILogger<GetAllCategoriesQueryHandler> logger
) : IRequestHandler<GetAllCategoriesQuery, IResult>
{
    public async Task<IResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.Subcategories)
                .ToListAsync(cancellationToken);

            if (!categories.Any())
                return Result<string>.NotFound("Категории не найдены.");

            var dtoList = Mapper.ToDtoList(categories);
            return Result<List<CategoryDto>>.Ok(dtoList);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении категорий");
            return Result<string>.BadRequest("Произошла ошибка при извлечении категорий.");
        }
    }
}
