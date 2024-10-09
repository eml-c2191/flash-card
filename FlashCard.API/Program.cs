using Serilog.Events;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Settings.Configuration;
using FlashCard.API.Extensions;
using FlashCard.Core;
using FlashCard.Business;
using FlashCard.Auth;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // same as appsettings.json
    .CreateBootstrapLogger();
try
{
    var bootstrapLogger = new SerilogLoggerFactory(Log.Logger)
    .CreateLogger(nameof(FlashCard.API.Extensions.ServiceCollectionExtensions));
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseSerilog((context, services, config) => config
        .ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions { SectionName = "Logging:Serilog" })
        .Enrich.FromLogContext()
        .WriteTo.Console()
    );
    builder.Services.AddAuthServices(builder.Configuration);
    builder.Services.AddEntityServices(builder.Configuration);
    builder.Services.AddBusinessServices();
    builder.Services.AddApiServices(builder.Configuration, builder.Environment);
    WebApplication app = builder.Build();

    app.Configure();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}