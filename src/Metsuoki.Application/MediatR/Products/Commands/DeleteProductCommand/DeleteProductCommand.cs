using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Products.Commands.DeleteProductCommand;
public record DeleteProductCommand(Guid Id) : IRequest<IResult>;
public class DeleteProductCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<DeleteProductCommand, IResult>
{
    public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([request.Id], cancellationToken);

        if (product == null)
            return Result<string>.NotFound("Товар не найден");

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return Result<string>.Ok("Товар удалён");
    }
}