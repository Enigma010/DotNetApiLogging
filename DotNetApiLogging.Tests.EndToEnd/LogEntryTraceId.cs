namespace DotNetApiLogging.Tests.E2e
{
    /// <summary>
    /// The log entry with the trace ID in it
    /// </summary>
    public class LogEntryTraceId : LogEntry
    {
        /// <summary>
        /// The trace ID
        /// </summary>
        public string TraceId { get; set; } = string.Empty;
    }
}
