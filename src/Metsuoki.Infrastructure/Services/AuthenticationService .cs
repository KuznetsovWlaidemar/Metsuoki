using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metsuoki.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwt;

    public AuthenticationService(
        UserManager<User> userManager,
        IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _jwt = jwtOptions.Value;
    }

    public async Task<string> CreateOrUpdateUserAndGenerateTokenAsync(string phoneNumber, string password)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        if (user == null)
        {
            user = new User
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
                FirstName = phoneNumber,
                LastName = phoneNumber 
            };
            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                throw new InvalidOperationException($"Не удалось создать пользователя: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            var passResult = await _userManager.AddPasswordAsync(user, password);
            if (!passResult.Succeeded)
                throw new InvalidOperationException(
                    $"Не удалось установить пароль: {string.Join(", ", passResult.Errors.Select(e => e.Description))}");
        }

        if (!user.PhoneNumberConfirmed)
        {
            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwt.Key);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName!)
                }),
            Expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes),
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}