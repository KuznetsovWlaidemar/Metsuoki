using System.Globalization;
using Metsuoki.API.Infrastructure;
using Metsuoki.Application;
using Metsuoki.Infrastructure;
using Metsuoki.Shared.ValidatorSettings;
using Serilog;
using Serilog.Events;

const string MYALLOWSPECIFICORIGINS = "_myAllowSpecificOrigins";
var cultureInfo = new CultureInfo("ru-RU");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
    .Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MYALLOWSPECIFICORIGINS, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services
    .AddMetsuokiApplication()
    .AddMetsuokiInfrastructure(configuration);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    });

if (!environment.IsProduction())
{
    builder.Services.AddApiGui();
    builder.Services.AddEndpointsApiExplorer();
}

builder.Services.AddFluentValidatorConfiguration();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtConfiguration(configuration);

builder.Logging.ClearProviders();

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.PostgreSQL(context.Configuration.GetConnectionString("MetsuokiConnectionWrite"),
                    "MetsuokiLogs",
                    needAutoCreateTable: true,
                    batchSizeLimit: 1,
                    restrictedToMinimumLevel: LogEventLevel.Warning));

Log.Information("Service Started");

var app = builder.Build();

app.AddApiGui();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(MYALLOWSPECIFICORIGINS);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();
