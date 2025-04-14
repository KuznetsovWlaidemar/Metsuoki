using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Categories.Commands.DeleteCategoryCommand;
public record DeleteCategoryCommand(Guid Id) : IRequest<IResult>;

public class DeleteCategoryCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<DeleteCategoryCommand, IResult>
{
    public async Task<IResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await context.Categories.FindAsync([request.Id], cancellationToken);

        if (category == null)
            return Result<string>.NotFound("Категория не найдена");

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);

        return Result<string>.Ok("Категория удалена!");
    }
}


