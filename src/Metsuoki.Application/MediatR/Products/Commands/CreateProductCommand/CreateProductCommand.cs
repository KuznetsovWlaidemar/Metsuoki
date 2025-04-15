using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Extensions;
using Metsuoki.Domain.Entities.Products;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Products.Commands.CreateProductCommand;
public record CreateProductCommand(CreateProductDto Dto) : IRequest<IResult>;
public class CreateProductCommandHandler(
    IMetsuokiDbContext context
) : IRequestHandler<CreateProductCommand, IResult>
{
    public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            BasePrice = dto.BasePrice,
            CategoryId = dto.CategoryId,
            DesignerId = dto.DesignerId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        var productDto = Mapper.ToDto(product);
        return Result<Guid>.Ok(product.Id);
    }
}