using System.Text.Json;
using System.Text.RegularExpressions;

namespace DotNetApiLogging
{
    /// <summary>
    /// Log search
    /// </summary>
    public class LogSearch
    {
        /// <summary>
        /// Creates a log search
        /// </summary>
        /// <param name="path"></param>
        public LogSearch(string path)
        {
            Path = path;
        }

        /// <summary>
        /// The path template (directory name + file name) to the log files
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Get log files
        /// </summary>
        /// <param name="directoryName">The directory name</param>
        /// <param name="fileName">The file name</param>
        /// <returns>The list of log files</returns>
        public List<string> GetLogFiles()
        {
            List<string> logFiles = new List<string>();
            string directoryName = System.IO.Path.GetDirectoryName(Path) ?? string.Empty;
            if (string.IsNullOrEmpty(directoryName))
            {
                return logFiles;
            }
            string fileName = System.IO.Path.GetFileName(Path);
            if (!Directory.Exists(directoryName))
            {
                return logFiles;
            }
            return Directory.GetFiles(directoryName, 
                System.IO.Path.GetFileNameWithoutExtension(fileName) + 
                "*" + 
                System.IO.Path.GetExtension(fileName)).ToList();
        }

        /// <summary>
        /// Gets log entries
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the log entry into</typeparam>
        /// <param name = "include" > The function on whether to include the log entry or not</param>
        /// <returns>The log entries</returns>
        public IEnumerable<T?> GetLogEntries<T>(Func<T?, bool>? include)
        {
            List<T?> logEntries = new List<T?>();
            foreach(var logFile in GetLogFiles())
            {
                logEntries.AddRange(GetLogEntries(logFile, include));
            }
            return logEntries;
        }

        /// <summary>
        /// Get log entries
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the log entry into</typeparam>
        /// <param name="path">The path log to the entries</param>
        /// <param name="include">The function on whether to include the log entry or not</param>
        /// <returns>The log entries</returns>
        public static IEnumerable<T?> GetLogEntries<T>(string path, Func<T?, bool>? include)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                do
                {
                    var line = reader.ReadLine() ?? string.Empty;
                    var logEntry = JsonSerializer.Deserialize<T>(line, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (include != null)
                    {
                        if (include(logEntry))
                        {
                            yield return logEntry;
                        }
                    }
                    else
                    {
                        yield return logEntry;
                    }
                } while (!reader.EndOfStream);
            }
        }

        /// <summary>
        /// Gets metadata (rootFile-YYYYMMDD.ext)
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns>bool - true/false if the parsing successed, string - rootFile, dateTime - log entyr, string - fileExtension</returns>
        public static (bool, string, DateTime?, string) GetLogFileDate(string fileName)
        {
            var regex = new Regex("^(?<fileRoot>.{1,})(?<year>[0-9]{4})(?<month>[0-9]{2})(?<day>[0-9]{2})(?<fileExtension>\\..{1,})$");
            var match = regex.Match(fileName);
            if (match.Success)
            {
                try
                {
                    DateTime logDate = new DateTime(
                        Int32.Parse(match.Groups["year"].Value),
                        Int32.Parse(match.Groups["month"].Value),
                        Int32.Parse(match.Groups["day"].Value));
                    return (true, match.Groups["fileRoot"].Value, logDate, match.Groups["fileExtension"].Value);
                }
                catch
                {
                    return (false, string.Empty, null, string.Empty);
                }
            }
            return (false, string.Empty, null, string.Empty);
        }
    }
}
