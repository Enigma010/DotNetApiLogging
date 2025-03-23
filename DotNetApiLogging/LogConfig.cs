using Serilog.Events;

namespace DotNetApiLogging
{
    /// <summary>
    /// The log configuration
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// The default directory name
        /// </summary>
        public const string DefaultDirectoryName = "Output";
        /// <summary>
        /// The default log file name
        /// </summary>
        public const string DefaultFileName = "log-.json";
        /// <summary>
        /// The path (directory and file name) of the log files. Note that the log files will be rotated per day
        /// so the actual log file name will have the date appended between the name of the file and the file
        /// extension
        /// </summary>
        public string Path { get; set; } = DefaultPathName();
        /// <summary>
        /// The minimum log level
        /// </summary>
        public LogEventLevel MinimumLogLevel { get; set; } = LogEventLevel.Information;
        /// <summary>
        /// Gets the default path
        /// </summary>
        /// <returns>The default path</returns>
        public static string DefaultPathName()
        {
            return System.IO.Path.Combine(DefaultDirectoryName, DefaultFileName);
        }
    }
}
