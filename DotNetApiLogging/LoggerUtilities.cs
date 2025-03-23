using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace DotNetApiLogging
{
    /// <summary>
    /// Logger utilities
    /// </summary>
    public static class LoggerUtilities
    {
        /// <summary>
        /// The source file pattern in the log entries
        /// </summary>
        public const string SourceFileNamePattern = "{sourceFileName}";
        /// <summary>
        /// The source file line number in the log entries
        /// </summary>
        public const string SourceLinePattern = "{sourceLineNumber}";
        /// <summary>
        /// The source member name (function) in the log entries
        /// </summary>
        public const string SourceMemberNamePattern = "{sourceMemberName}";
        /// <summary>
        /// The log entry message
        /// </summary>
        public const string LogCallerPattern = $"Source file: {SourceFileNamePattern}, source line #: {SourceLinePattern}, source member name: {SourceMemberNamePattern}";
        /// <summary>
        /// Logs a trace entry, tyically called like this:
        ///    log.LogTraceCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogTraceCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogTrace(msg, args), args);
        }
        /// <summary>
        /// Logs a information entry, tyically called like this:
        ///    log.LogInformationCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogInformationCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogInformation(msg, args), args);
        }
        /// <summary>
        /// Logs a information entry, tyically called like this:
        ///    log.LogInformationCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogDebugCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogDebug(msg, args), args);
        }
        /// <summary>
        /// Logs a trace entry, tyically called like this:
        ///    log.LogWarningCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogWarningCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogWarning(msg, args), args);
        }
        /// <summary>
        /// Logs a critical entry, tyically called like this:
        ///    log.LogCriticalCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogCriticalCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogCritical(msg, args), args);
        }
        /// <summary>
        /// Logs a error entry, tyically called like this:
        ///    log.LogErrorCaller("Log message {value}", args: [1]);
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="args">The args to pass into the message</param>
        public static void LogErrorCaller(this ILogger logger, string message,
            [CallerFilePath] string sourceFileName = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            [CallerMemberName] string sourceMemberName = "",
            params object[] args)
        {
            LogCaller(message, sourceFileName, sourceLineNumber, sourceMemberName, (msg, args) => logger.LogError(msg, args), args);
        }
        /// <summary>
        /// Logs an entry using a log action
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="sourceFileName">The file name (don't pass anthing in here)</param>
        /// <param name="sourceLineNumber">The line number (don't pass anyting in here)</param>
        /// <param name="sourceMemberName">The member name (don't pass anything in here)</param>
        /// <param name="log">The action to log the entry</param>
        /// <param name="args">The args to pass into the message</param>
        private static void LogCaller(string message, string sourceFileName, int sourceLineNumber, 
            string sourceMemberName, Action<string, object[]> log, params object[] args)
        {
            string completeMessage = $"{LogCallerPattern}: {message}";
            log(completeMessage, [sourceFileName, sourceLineNumber, sourceMemberName, .. args]);
        }
    }
}
