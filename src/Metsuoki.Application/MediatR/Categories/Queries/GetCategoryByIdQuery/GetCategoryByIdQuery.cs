using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Application.MediatR.Categories.Queries.GetAllCategoriesQuery;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.EntityFrameworkCore;

namespace Metsuoki.Application.MediatR.Categories.Queries.GetCategoryByIdQuery;
public record GetCategoryByIdQuery(Guid Id) : IRequest<IResult>;

public class GetCategoryByIdQueryHandler(
    IMetsuokiDbContext context
) : IRequestHandler<GetCategoryByIdQuery, IResult>
{
    public async Task<IResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category == null)
            return Result<string>.NotFound("Категория не найдена");

        var result = Mapper.ToDto(category);

        return Result<CategoryDto>.Ok(result);
    }
}