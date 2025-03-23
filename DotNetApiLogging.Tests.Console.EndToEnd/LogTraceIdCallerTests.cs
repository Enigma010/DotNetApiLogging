using Microsoft.Extensions.Logging;

namespace DotNetApiLogging.Tests.Console.E2e
{
    class LogTraceIdCallerTests
    {
        public static void Trace(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogTraceCaller(message: message, args: [traceId]);
            }
        }
        public static void Information(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogInformationCaller(message: message, args: [traceId]);
            }
        }
        public static void Debug(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogDebugCaller(message: message, args: [traceId]);
            }
        }
        public static void Warning(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogWarningCaller(message: message, args: [traceId]);
            }
        }
        public static void Error(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogErrorCaller(message: message, args: [traceId]);
            }
        }
        public static void Critical(ILogger logger, string message, string traceId)
        {
            if (!string.IsNullOrEmpty(traceId))
            {
                logger.LogCriticalCaller(message: message, args: [traceId]);
            }
        }
    }
}
