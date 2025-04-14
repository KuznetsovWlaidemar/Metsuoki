using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Categories.Commands.UpdateCategoryCommand;

public record UpdateCategoryCommand(UpdateCategoryDto Dto) : IRequest<IResult>;
public class UpdateCategoryCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<UpdateCategoryCommand, IResult>
{
    public async Task<IResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var category = await context.Categories.FindAsync([dto.Id], cancellationToken);

        if (category == null)
            return Result<string>.NotFound("Категория не найдена");

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.ParentCategoryId = dto.ParentCategoryId;

        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Ok("Категория изменена");
    }
}
