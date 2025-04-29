using System;

namespace Metsuoki.Application.Common.Interfaces;

public interface IAuthenticationService
{
    /// <returns>JWT-токен</returns>
    Task<string> CreateOrUpdateUserAndGenerateTokenAsync(string phoneNumber, string password);
}