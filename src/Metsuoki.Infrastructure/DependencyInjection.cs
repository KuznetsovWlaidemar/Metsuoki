using Metsuoki.Application.Common.Interfaces;
using Metsuoki.Domain.Identity;
using Metsuoki.Infrastructure.Persistence;
using Metsuoki.Infrastructure.Services;
using Metsuoki.Shared;
using Metsuoki.Shared.Interfaces;
using Metsuoki.Shared.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metsuoki.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMetsuokiInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMetsuokiContext(configuration)
                .AddExternalServices(configuration)
                .AddTransient<ITokenService, TokenService>();

        return services;
    }

    private static IServiceCollection AddMetsuokiContext(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<MetsuokiDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("MetsuokiConnectionWrite")))
            .AddScoped<IMetsuokiDbContext>(provider => provider.GetService<MetsuokiDbContext>());

        services.AddIdentityCore<User>(opts =>
        {
            opts.Lockout.AllowedForNewUsers = false;
            opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            opts.Lockout.MaxFailedAccessAttempts = 3;
            opts.Password.RequiredLength = 8;
            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireDigit = false;
        })
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<MetsuokiDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}