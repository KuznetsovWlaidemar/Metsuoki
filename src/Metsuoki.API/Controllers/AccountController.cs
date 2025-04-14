using Metsuoki.Application.MediatR.Account.Commands.RegistrationCommand;
using Metsuoki.Application.MediatR.Tokens.CreateToken;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Controllers;

/// <summary>
/// Контроллер для аутентификации и регистрации пользователей.
/// </summary>
[Produces("application/json")]
public class AccountController : BaseController
{
    /// <summary>
    /// Регистрирует нового пользователя
    /// </summary>
    /// <param name="command">Данные для регистрации пользователя.</param>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationCommand command)
    {
        var result = await Mediator.Send(command);

        return result;
    }

    /// <summary>
    /// Аутентифицирует пользователя
    /// </summary>
    /// <param name="command">Команда с учетными данными пользователя для аутентификации.</param>
    /// <returns>
    /// Возвращает JWT-токен в случае успешной аутентификации (код 200 OK) 
    /// или массив ошибок в случае неудачи (код 400 Bad Request).
    /// </returns>
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Authenticate([FromBody] CreateTokenCommand command)
    {
        var result = await Mediator.Send(command);

        return result;
    }
}