using Microsoft.Extensions.Logging;

namespace DotNetApiLogging.Tests.E2e
{
    /// <summary>
    /// Console end to end tests, calls the DotNetApiLogging.Tests.Console.E2e program
    /// with various log trace configurations and validates that the logs contain the predicted log
    /// messages
    /// </summary>
    public class ConsoleEndToEndTests
    {
        public const string TraceIdLogMessage = "Trace Id: {traceId}";

        [Fact]
        public void Trace()
        {
            TestLegLevelSingleLogEntry(LogLevel.Trace, traceIdTraceArgument);
        }

        [Fact]
        public void Information()
        {
            TestLegLevelSingleLogEntry(LogLevel.Information, traceIdInformationArgument);
        }

        [Fact]
        public void Debug()
        {
            TestLegLevelSingleLogEntry(LogLevel.Debug, traceIdDebugArgument);
        }

        [Fact]
        public void Warning()
        {
            TestLegLevelSingleLogEntry(LogLevel.Warning, traceIdWarningArgument);
        }

        [Fact]
        public void Error()
        {
            TestLegLevelSingleLogEntry(LogLevel.Error, traceIdErrorArgument);
        }
        [Fact]
        public void Critical()
        {
            TestLegLevelSingleLogEntry(LogLevel.Critical, traceIdCriticalArgument);
        }

        private string ScratchDirectoryRoot()
        {
            return System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Output");
        }

        private void RunProcess(ScratchDirectory scratchDirectory, LogLevel logLevel, params string[] additionalArguments)
        {
            string arguments = $"{BaseArguments(logLevel)} {LogPath(Path.Combine(scratchDirectory.Directory, LogConfig.DefaultFileName))} ";
            foreach(var additionalArgument in additionalArguments)
            {
                arguments += additionalArgument;
            }
            var process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = arguments
            };
            process.Start();
            process.WaitForExit();
            Assert.Equal(0, process.ExitCode);
        }

        private void TestLegLevelSingleLogEntry(LogLevel logLevel, Func<string, string> traceIdArgumentFunc)
        {
            // generate a unique trace ID, we'll search the log file for this value
            string traceId = Guid.NewGuid().ToString();
            using (var scratchDirectory = new ScratchDirectory(ScratchDirectoryRoot()))
            {
                // Run the conole E2e with the trace ID we generated, validates that the process finishes with
                // the correct non-error code
                RunProcess(scratchDirectory, logLevel, traceIdArgumentFunc(traceId));
                // Creates a new log search utility
                var logSearch = new LogSearch(System.IO.Path.Combine(scratchDirectory.Directory, LogConfig.DefaultFileName));
                // Gets all the log entries from the directory with the trace ID that we invoked for the test program
                // with
                var logEntries = logSearch.GetLogEntries<LogEntryTraceId>((logEntry) => logEntry != null && logEntry.TraceId == traceId);
                // Validates that we found one and only one log entry
                Assert.Single(logEntries);
            }
        }

        private string ConsoleE2ePath()
        {
            return System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "DotNetApiLogging.Tests.Console.EndToEnd", "DotNetApiLogging.Tests.Console.EndToEnd.csproj");
        }

        private string BaseArguments(LogLevel logLevel)
        {
            return $"run --no-build --no-launch-profile --project {ConsoleE2ePath()} --logLevel {(int)logLevel} {TraceIdMessageArgument()}";
        }

        private string LogPath(string logPath)
        {
            return $"--logPath {logPath}";
        }

        private string TraceIdMessageArgument()
        {
            return "--traceIdMessge \""+ TraceIdLogMessage + "\"";
        }

        private string traceIdInformationArgument(string id)
        {
            return traceIdArgument("Information", id);
        }

        private string traceIdTraceArgument(string id)
        {
            return traceIdArgument("Trace", id);
        }

        private string traceIdDebugArgument(string id)
        {
            return traceIdArgument("Debug", id);
        }


        private string traceIdWarningArgument(string id)
        {
            return traceIdArgument("Warning", id);
        }

        private string traceIdErrorArgument(string id)
        {
            return traceIdArgument("Error", id);
        }

        private string traceIdCriticalArgument(string id)
        {
            return traceIdArgument("Critical", id);
        }

        private string traceIdArgument(string type, string id)
        {
            return $"--traceId{type} {id}";
        }
    }
}