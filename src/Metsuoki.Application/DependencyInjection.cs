using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Metsuoki.Application.MediatR.PostProcessors;
using Microsoft.Extensions.DependencyInjection;

namespace Metsuoki.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddMetsuokiApplication(this IServiceCollection services)
    {
        services
              .AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
              .AddFluentValidationAutoValidation()
              .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(UserActionLogPostProcessor<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
        return services;
    }
}