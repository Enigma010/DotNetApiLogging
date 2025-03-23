// See https://aka.ms/new-console-template for more information
using CommandLine;
using DotNetApiLogging.Di;
using DotNetApiLogging.Tests.Console.E2e;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetApiLogging.Console.Tests.E2e;

class Program
{
    /// <summary>
    /// A console application used to test logging, the way it's invoked is that it initialized the logger
    /// to a specific log level, the based on the arguments sent to it it logs various types of messages
    /// (informational, debug, trace, etc), then it stops.  The calling program then probes the log files
    /// created and verifies that log messages and log message contents match with what's expected
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static int Main(string[] args)
    {
        try
        {
            var parsedArguments = Parser.Default.ParseArguments<Options>(args);
            parsedArguments.WithParsed<Options>(options =>
            {
                string OutputDirectoryName = Directory.GetCurrentDirectory();
                string OutputRootDirectory = System.IO.Path.Combine(Directory.GetCurrentDirectory(), OutputDirectoryName);
                if (!System.IO.Directory.Exists(OutputRootDirectory))
                {
                    System.IO.Directory.CreateDirectory(OutputRootDirectory);
                }
                // Create an application and configure the logger based on the passed in parameters
                var appBuilder = Host.CreateApplicationBuilder();
                var Config = new LogConfig()
                {
                    Path = options.LogPath,
                    MinimumLogLevel = options.LogLevel
                };
                appBuilder.AddLoggerDependencies(Config);
                var App = appBuilder.Build();

                ILogger logger = App.Services.GetService(typeof(ILogger<Program>)) as ILogger<Program> ?? throw new InvalidOperationException();

                // Invokes the various types of logs trace, debug, information, etc based on the parameters passed in
                LogTraceIdCallerTests.Trace(logger, options.TraceIdMessage, options.TraceIdTrace);
                LogTraceIdCallerTests.Debug(logger, options.TraceIdMessage, options.TraceIdDebug);
                LogTraceIdCallerTests.Information(logger, options.TraceIdMessage, options.TraceIdInformation);
                LogTraceIdCallerTests.Warning(logger, options.TraceIdMessage, options.TraceIdWarning);
                LogTraceIdCallerTests.Error(logger, options.TraceIdMessage, options.TraceIdError);
                LogTraceIdCallerTests.Critical(logger, options.TraceIdMessage, options.TraceIdCritical);
            });
            // In case the program was invoked with the wrong parameters set the return value to -1 so the calling
            // program can detect that an error occurred
            return (parsedArguments.Errors.Count() > 0) ? -1 : 0;
        }
        catch
        {
            return -1;
        }
    }
}