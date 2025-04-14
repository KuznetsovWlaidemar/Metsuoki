using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Domain.Entities;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Categories.Commands.CreateCategoryCommand;
public record CreateCategoryCommand(CreateCategoryDto Dto) : IRequest<IResult>;

public class CreateCategoryCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<CreateCategoryCommand, IResult>
{
    public async Task<IResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            ParentCategoryId = dto.ParentCategoryId,
            Subcategories = new List<Category>()
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Ok(category.Id);
    }
}
