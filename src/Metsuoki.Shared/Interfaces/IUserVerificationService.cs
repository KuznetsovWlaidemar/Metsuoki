namespace Metsuoki.Shared.Interfaces;
public interface IUserVerificationService
{
    string GenerateOtpCode();
    Task SaveCodeAsync(string phoneNumber, string code);
    Task<bool> VerifyCodeAsync(string phoneNumber, string code);
    Task SendSmsAsync(string phoneNumber, string message);
}