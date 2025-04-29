using Metsuoki.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Infrastructure.Services;

/// <summary>
/// Пока не выбрали провайдера — просто логируем или бросаем, чтобы не забыть.
/// </summary>
public class SmsSenderStub : ISmsSender
{
    private readonly ILogger<SmsSenderStub> _log;
    public SmsSenderStub(ILogger<SmsSenderStub> log) => _log = log;

    public Task SendAsync(string phoneNumber, string message)
    {
        _log.LogWarning("SMS to {Phone}: {Text}", phoneNumber, message);
        // TODO: заменить на реальную интеграцию
        return Task.CompletedTask;
    }
}