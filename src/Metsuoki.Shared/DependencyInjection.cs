using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metsuoki.Shared;
public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserVerificationService, UserVerificationService>();
        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        services.AddSingleton(provider => new SmsClient("your_api_key"));

        return services;
    }
}
