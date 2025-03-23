using CommandLine;
using CommandLine.Text;
using Serilog.Events;

namespace DotNetApiLogging.Tests.Console.E2e
{
    public class Options
    {
        [Option("logPath", Required = true, HelpText = "The path (directory and file name) where to put log entries")]
        public string LogPath { get; set; } = string.Empty;
        [Option("logLevel", Required = true, HelpText = "The path (directory and file name) where to put log entries")]
        public LogEventLevel LogLevel { get; set; } = LogEventLevel.Verbose;
        [Option("traceIdMessge", Required = true, HelpText = "The message to use in the trace file")]
        public string TraceIdMessage { get; set; } = string.Empty;
        [Option("traceIdTrace", Required = false, HelpText = "If set it will log an entry of type trace using this trace ID")]
        public string TraceIdTrace { get; set; } = string.Empty;
        [Option("traceIdInformation", Required = false, HelpText = "If set it will log an entry of type information using this trace ID")]
        public string TraceIdInformation { get; set; } = string.Empty;
        [Option("traceIdDebug", Required = false, HelpText = "If set it will log an entry of type debug using this trace ID")]
        public string TraceIdDebug { get; set; } = string.Empty;        
        [Option("traceIdWarning", Required = false, HelpText = "If set it will log an entry of type warning using this trace ID")]
        public string TraceIdWarning { get; set; } = string.Empty;
        [Option("traceIdError", Required = false, HelpText = "If set it will log an entry of type error using this trace ID")]
        public string TraceIdError { get; set; } = string.Empty;
        [Option("traceIdCritical", Required = false, HelpText = "If set it will log an entry of type critical using this trace ID")]
        public string TraceIdCritical { get; set; } = string.Empty;
    }
}
