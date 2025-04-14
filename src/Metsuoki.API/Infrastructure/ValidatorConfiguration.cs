using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Metsuoki.Shared.ValidatorSettings;
using Microsoft.AspNetCore.Mvc;

namespace Metsuoki.API.Infrastructure;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddFluentValidatorConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddScoped<ValidationFilter>();

        services.AddFluentValidationAutoValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;

        });

        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
};