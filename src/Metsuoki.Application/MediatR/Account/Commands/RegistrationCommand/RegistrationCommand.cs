using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Domain.Identity;
using Metsuoki.Shared.Constants;
using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Metsuoki.Application.MediatR.Account.Commands.RegistrationCommand;

/// <summary>
/// Команда для регистрации нового пользователя.
/// </summary>
/// <param name="PhoneNumber">Номер телефона пользователя.</param>
public record RegistrationCommand(
    string PhoneNumber
) : IRequest<IResult>;

public class RegistrationCommandHandler(
    IUserVerificationService verificationService, // Сервис отправки кода и сохранения его в хранилище
    UserManager<User> userManager,
    ILogger<RegistrationCommandHandler> logger
) : IRequestHandler<RegistrationCommand, IResult>
{
    public async Task<IResult> Handle(RegistrationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // Проверяем формат номера телефона
            if (!IsValidRussianPhoneNumber(command.PhoneNumber))
            {
                return Result<string>.BadRequest("Неверный формат номера телефона.");
            }

            var isPhoneNumberExists = await userManager.Users
                .AnyAsync(u => u.PhoneNumber == command.PhoneNumber, cancellationToken);

            if (isPhoneNumberExists)
            {
                return Result<string>.BadRequest("Пользователь с таким номером телефона уже зарегистрирован.");
            }

            var otpCode = verificationService.GenerateOtpCode();

            await verificationService.SaveCodeAsync(command.PhoneNumber, otpCode);
            await verificationService.SendSmsAsync(command.PhoneNumber, $"Ваш код подтверждения: {otpCode}");

            return Result<string>.Ok("Код подтверждения отправлен.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Ошибка при отправке кода подтверждения");
            return Result<string>.BadRequest("Не удалось отправить код подтверждения.");
        }
    }

    private bool IsValidRussianPhoneNumber(string phoneNumber)
    {
        // Проверка, что номер начинается с +7 и состоит из 12 цифр
        return phoneNumber.StartsWith("+7") && phoneNumber.Length == 12 && phoneNumber.Skip(2).All(char.IsDigit);
    }
}