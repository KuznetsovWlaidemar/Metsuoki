using Metsuoki.Application.MediatR.Categories.Commands.CreateCategoryCommand;
using Metsuoki.Application.MediatR.Categories.Commands.DeleteCategoryCommand;
using Metsuoki.Application.MediatR.Categories.Commands.UpdateCategoryCommand;
using Metsuoki.Application.MediatR.Categories.Queries.GetAllCategoriesQuery;
using Metsuoki.Application.MediatR.Categories.Queries.GetCategoryByIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Controllers;

/// <summary>
/// Контроллер для работы с категориями
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CategoryController : BaseController
{
    /// <summary>Получить все категории</summary>
    [HttpGet("get-all-category")]
    public async Task<IActionResult> GetAllCategory()
        => Ok(await Mediator.Send(new GetAllCategoriesQuery()));

    /// <summary>Получить категорию по Id</summary>
    [HttpGet("get-category-by-id")]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
        => Ok(await Mediator.Send(new GetCategoryByIdQuery(id)));

    /// <summary>Создать категорию</summary>
    [HttpPost("create-category")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        => Ok(await Mediator.Send(new CreateCategoryCommand(dto)));

    /// <summary>Обновить категорию</summary>
    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
    {
        return Ok(await Mediator.Send(new UpdateCategoryCommand(dto)));
    }

    /// <summary>Удалить категорию</summary>
    [HttpDelete("delete-category")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await Mediator.Send(new DeleteCategoryCommand(id)));
}