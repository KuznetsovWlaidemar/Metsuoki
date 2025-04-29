using System;

namespace Metsuoki.Application.Common.Interfaces;

/// <summary>
/// Отправка SMS-сообщений (абстракция над реальным провайдером).
/// </summary>
public interface ISmsSender
{
    /// <param name="phoneNumber">Телефон в формате E.164 (+7...)</param>
    /// <param name="message">Текст SMS</param>
    Task SendAsync(string phoneNumber, string message);
}