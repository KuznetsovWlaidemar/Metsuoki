using System.Net.Mail;
using Metsuoki.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Shared.Services;
public class UserVerificationService : IUserVerificationService
{
    private readonly ILogger<UserVerificationService> _logger;
    private readonly IRedisCacheService _cacheService;
    private readonly SmsClient _smsClient;

    public UserVerificationService(ILogger<UserVerificationService> logger, IRedisCacheService cacheService, SmsClient smsClient)
    {
        _logger = logger;
        _cacheService = cacheService;
        _smsClient = smsClient;
    }

    public string GenerateOtpCode()
    {
        var rng = new Random();
        return rng.Next(100000, 999999).ToString(); // Генерация случайного 6-значного кода
    }

    public async Task SaveCodeAsync(string phoneNumber, string code)
    {
        try
        {
            // Сохраняем код в Redis с временем жизни 5 минут
            await _cacheService.SetAsync(phoneNumber, code, TimeSpan.FromMinutes(5));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при сохранении OTP кода");
            throw;
        }
    }

    public async Task<bool> VerifyCodeAsync(string phoneNumber, string code)
    {
        try
        {
            // Получаем сохранённый код из Redis
            var savedCode = await _cacheService.GetAsync<string>(phoneNumber);
            return savedCode == code;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при верификации OTP кода");
            return false;
        }
    }

    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            // Используем интеграцию с SMS.ru для отправки сообщения
            await _smsClient.SendSmsAsync(phoneNumber, message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при отправке SMS");
            throw;
        }
    }
}

