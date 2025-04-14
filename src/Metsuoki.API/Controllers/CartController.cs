using Metsuoki.Application.MediatR.Carts.Commands.AddToCartCommand;
using Metsuoki.Application.MediatR.Carts.Commands.RemoveFromCartCommand;
using Metsuoki.Application.MediatR.Carts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Controllers;

/// <summary>
/// Контроллер для работы с корзиной
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CartController : BaseController
{
    /// <summary>
    /// Получает корзину пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    [HttpGet("get-user-cart")]
    public async Task<IActionResult> GetUserCartQuery([FromQuery] Guid userId)
    {
        var result = await Mediator.Send(new GetUserCartQuery(userId));
        return Ok(result);
    }

    /// <summary>
    /// Добавить товар в корзину
    /// </summary>
    [HttpPost("add-product-to-cart")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    /// <summary>
    /// Удалить товар из корзины
    /// </summary>
    [HttpDelete("remove-product-from-cart")]
    public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }
}