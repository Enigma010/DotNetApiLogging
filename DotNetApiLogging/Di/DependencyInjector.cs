using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Serilog.Formatting.Compact;
using Serilog.Events;

namespace DotNetApiLogging.Di
{
    public static class DependencyInjector
    {
        public static void AddLoggerDependencies(this IHostApplicationBuilder builder, LogConfig config)
        {
            builder.Services.AddSerilog((loggerConfig) =>
            {
                loggerConfig.WriteTo.Console()
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    config.Path,
                    rollingInterval: RollingInterval.Day)
                .Enrich.WithMachineName();
                switch (config.MinimumLogLevel)
                {
                    case LogEventLevel.Information:
                        loggerConfig = loggerConfig.MinimumLevel.Information();
                        break;
                    case LogEventLevel.Verbose:
                        loggerConfig = loggerConfig.MinimumLevel.Verbose();
                        break;
                    case LogEventLevel.Debug:
                        loggerConfig = loggerConfig.MinimumLevel.Debug();
                        break;
                    case LogEventLevel.Warning:
                        loggerConfig = loggerConfig.MinimumLevel.Warning();
                        break;
                    case LogEventLevel.Error:
                        loggerConfig = loggerConfig.MinimumLevel.Error();
                        break;
                    case LogEventLevel.Fatal:
                        loggerConfig = loggerConfig.MinimumLevel.Fatal();
                        break;
                }
            });
        }
        public static void AddWebLoggingDependencies(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
        }
    }
}
