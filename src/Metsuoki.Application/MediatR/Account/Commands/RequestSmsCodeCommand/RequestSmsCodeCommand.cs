using MediatR;
using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Account.Commands.RequestSmsCodeCommand;

public record RequestSmsCodeCommand(string PhoneNumber) : IRequest<IResult<string>>;

public class RequestSmsCodeCommandHandler : IRequestHandler<RequestSmsCodeCommand, IResult<string>>
{
    private readonly IUserVerificationService _verifier;

    public RequestSmsCodeCommandHandler(IUserVerificationService verifier) => _verifier = verifier;

    public async Task<IResult<string>> Handle(RequestSmsCodeCommand request,CancellationToken ct)
    {
        var code = _verifier.GenerateOtpCode();
        await _verifier.SaveCodeAsync(request.PhoneNumber, code);
        await _verifier.SendSmsAsync(request.PhoneNumber, $"Ваш код подтверждения: {code}");

        return Result<string>.Ok("Код отправлен на указанный номер");
    }
}