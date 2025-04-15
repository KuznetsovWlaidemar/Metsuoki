using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Products.Commands.UpdateProductCommand;
public record UpdateProductCommand(UpdateProductDto Dto) : IRequest<IResult>;

public class UpdateProductCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<UpdateProductCommand, IResult>
{
    public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([request.Dto.Id], cancellationToken);

        if (product is null)
            return Result<string>.NotFound("Товар не найден");

        var dto = request.Dto;

        product.Title = dto.Title;
        product.Description = dto.Description;
        product.BasePrice = dto.BasePrice;
        product.CategoryId = dto.CategoryId;
        product.DesignerId = dto.DesignerId;

        await context.SaveChangesAsync(cancellationToken);
        return Result<string>.Ok("Товар обновлён");
    }
}
