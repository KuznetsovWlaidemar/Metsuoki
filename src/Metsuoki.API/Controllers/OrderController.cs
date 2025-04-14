using Metsuoki.Application.MediatR.Order.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{
    /// <summary>
    /// Получает список всех заказов
    /// </summary>
    /// <returns>Список заказов</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllOrdersQuery());
        return Ok(result);
    }
}