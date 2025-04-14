using System.Security.Claims;
using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.MediatR.Tokens.Dtos;
using Metsuoki.Domain.Identity;
using Metsuoki.Shared.Constants;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Tokens.CreateToken;

/// <summary>
/// Команда для создания JWT-токена на основе учетных данных пользователя и привязки к мини-маркету.
/// </summary>
public class CreateTokenCommand : IRequest<IResult>
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string Password { get; set; }
}

public class CreateTokenCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IMetsuokiDbContext context,
    ITokenService tokenService,
    ILogger<CreateTokenCommandHandler> logger
) : IRequestHandler<CreateTokenCommand, IResult>
{
    public async Task<IResult> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByNameAsync(command.Username);
            if (user == null)
                return Result<string>.BadRequest("Пользователь не найден.");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, command.Password);
            if (!isPasswordValid)
                return Result<string>.BadRequest("Неверное имя пользователя или пароль.");

            var claims = new List<Claim>();
            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(new[]
           {
                new Claim(CustomClaimTypes.FullName, user.FullName),
                new Claim(CustomClaimTypes.FirstName, user.FirstName),
                new Claim(CustomClaimTypes.LastName, user.LastName),
                new Claim(CustomClaimTypes.Patronymic, user.Patronymic ?? ""),
                new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber ?? "")
            });


            var tokenDto = new TokenDto
            {
                AuthToken = tokenService.GenerateJwtToken(user, claims),
                RefreshToken = tokenService.GenerateRefreshToken()
            };

            await context.RefreshTokens.AddAsync(new RefreshToken
            {
                Token = tokenDto.RefreshToken,
                ExpiryDate = DateTime.UtcNow.AddDays(1),
                UserId = user.Id,
                IsRevoked = false
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return Result<TokenDto>.Ok(tokenDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Возникли ошибки при генерации токена");
            return Result<string>.BadRequest("Возникли ошибки при аутентификации");
        }

    }
}