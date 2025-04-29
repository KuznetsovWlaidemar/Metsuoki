using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Infrastructure.Services;

public class UserVerificationService : IUserVerificationService
{
    private readonly ILogger<UserVerificationService> _logger;
    private readonly IRedisCacheService _cacheService;
    private readonly ISmsSender _smsSender;

    public UserVerificationService(
        ILogger<UserVerificationService> logger,
        IRedisCacheService cacheService,
        ISmsSender smsSender)
    {
        _logger = logger;
        _cacheService = cacheService;
        _smsSender = smsSender;
    }

    public string GenerateOtpCode()
    {
        var rng = new Random();
        return rng.Next(100000, 999999).ToString();
    }

    public async Task SaveCodeAsync(string phoneNumber, string code)
    {
        await _cacheService.SetAsync(phoneNumber, code, TimeSpan.FromMinutes(5));
    }

    public async Task<bool> VerifyCodeAsync(string phoneNumber, string code)
    {
        var saved = await _cacheService.GetAsync<string>(phoneNumber);
        return saved == code;
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            await _smsSender.SendAsync(phoneNumber, message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при отправке SMS");
            throw;
        }
    }
}