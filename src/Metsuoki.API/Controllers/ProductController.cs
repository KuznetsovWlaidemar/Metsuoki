using Metsuoki.Application.MediatR.Products.Commands.CreateProductCommand;
using Metsuoki.Application.MediatR.Products.Commands.DeleteProductCommand;
using Metsuoki.Application.MediatR.Products.Commands.UpdateProductCommand;
using Metsuoki.Application.MediatR.Products.Queries.GetAllProductsQuery;
using Metsuoki.Application.MediatR.Products.Queries.GetProductByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Controllers;
/// <summary>
/// Контроллер для работы с товарами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseController
{
    /// <summary>
    /// Получает список всех товаров
    /// </summary>
    /// <returns>Список товаров</returns>
    [HttpGet("get-all-products")]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Получает товар по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns>Товар</returns>
    [HttpGet("get-product-by-id")]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var result = await Mediator.Send(new GetProductByIdQuery(id));
        return result is not null ? Ok(result) : NotFound();
    }

    /// <summary>
    /// Добавляет новый товар
    /// </summary>
    /// <param name="command">Команда для добавления товара</param>
    /// <returns>Результат добавления товара</returns>
    [HttpPost("create-product")]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    /// <summary>
    /// Обновляет существующий товар
    /// </summary>
    /// <param name="command">Команда для обновления товара</param>
    /// <returns>Результат обновления товара</returns>
    [HttpPut("update-product")]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    /// <summary>
    /// Удаляет товар по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns>Результат удаления товара</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteProductCommand(id));
        return result;
    }
}
