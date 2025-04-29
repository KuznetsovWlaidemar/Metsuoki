using MediatR;
using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Application.Dtos;
using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Interfaces.Result;

namespace Metsuoki.Application.MediatR.Account.Commands.ConfirmSmsCodeCommand;

public record ConfirmSmsCodeCommand(string PhoneNumber, string Code, string Password) : IRequest<IResult<AuthResponseDto>>;

public class ConfirmSmsCodeCommandHandler : IRequestHandler<ConfirmSmsCodeCommand, IResult<AuthResponseDto>>
{
    private readonly IUserVerificationService _verifier;
    private readonly IAuthenticationService _auth;

    public ConfirmSmsCodeCommandHandler(
        IUserVerificationService verifier,
        IAuthenticationService auth)
    {
        _verifier = verifier;
        _auth = auth;
    }

    public async Task<IResult<AuthResponseDto>> Handle(
        ConfirmSmsCodeCommand request, CancellationToken ct)
    {
        if (!await _verifier.VerifyCodeAsync(request.PhoneNumber, request.Code))
        return Result<AuthResponseDto>.BadRequest("Неверный код подтверждения");

        var token = await _auth.CreateOrUpdateUserAndGenerateTokenAsync(request.PhoneNumber, request.Password);

        var response = new AuthResponseDto(token);
        return Result<AuthResponseDto>.Ok("Успешно", response);
    }

}