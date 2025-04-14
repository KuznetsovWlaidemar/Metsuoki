using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Shared.Core.Models;

namespace Metsuoki.API.Infrastructure;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddApiGui(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.Configure<ScalarOptions>(options => options.Title = "API");

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "Введите токен целиком в формате: Bearer <ваш_токен>"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1"
            });

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "api-doc.xml");
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    public static void AddApiGui(this WebApplication app)
    {
        app.MapOpenApi();

        app.UseSwagger(c =>
        {
            c.RouteTemplate = $"api/{{documentName}}/api.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = $"/api";
            c.SwaggerEndpoint($"/api/v1/api.json", "API");
        });

        app.MapScalarApiReference(options =>
        {
            options
                .WithPreferredScheme("Bearer")
                .WithHttpBearerAuthentication(bearer =>
                {
                    bearer.Token = "your-bearer-token";
                });

            options.WithOpenApiRoutePattern($"/api/v1/api.json");
            options.WithEndpointPrefix($"/api/{{documentName}}");

            options
                .WithTitle("API")
                .WithSidebar(true);

            options.Title = "API";
            options.ShowSidebar = true;
        });
    }

    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"])),
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}