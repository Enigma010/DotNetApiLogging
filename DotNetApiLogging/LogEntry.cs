namespace DotNetApiLogging
{
    /// <summary>
    /// The log entry
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The source file where the log entry was generated from
        /// </summary>
        public string SourceFileName { get; set; } = string.Empty;
        /// <summary>
        /// The line numer in the source file where this exception was logged
        /// </summary>
        public int SourceLineNumber { get; set; } = 0;
        /// <summary>
        /// The member or method that this log entry was generated
        /// </summary>
        public string SourceMemberName { get; set; } = string.Empty;
    }
}
