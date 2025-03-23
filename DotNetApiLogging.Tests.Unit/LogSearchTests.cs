using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetApiLogging.Tests.Unit
{
    public class LogSearchTests : IDisposable
    {
        private readonly ScratchDirectory _scratchDirectory;
        private readonly LogSearch _logSearch;
        private bool disposedValue;

        public LogSearchTests()
        {
            _scratchDirectory = new ScratchDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Output"));
            _logSearch = new LogSearch(Path.Combine(_scratchDirectory.Directory, LogConfig.DefaultFileName));
        }

        [Fact]
        public void GetLogFiles()
        {
            string logFilePrefix = Path.GetFileNameWithoutExtension(LogConfig.DefaultFileName);
            string logFileExtension = Path.GetExtension(LogConfig.DefaultFileName);
            List<DateTime> logFilesDates = new List<DateTime>()
            {
                new DateTime(2024, 1, 29),
                new DateTime(2022, 2, 6),
                new DateTime(2025, 10, 1),
                new DateTime(2025, 12, 19),
            };
            logFilesDates.ForEach(logFileDate =>
            {
                File.Create(Path.Combine(_scratchDirectory.Directory, $"{logFilePrefix}{logFileDate.Year}{logFileDate.Month.ToString().PadLeft(2, '0')}{logFileDate.Day.ToString().PadLeft(2, '0')}{logFileExtension}"))
                .Close();
            });
            List<string> logFiles = _logSearch.GetLogFiles();
            Assert.Equal(logFilesDates.Count, logFiles.Count);
        }

        [Theory]
        [InlineData($"logFile-20250101.json", true, "logFile-", 2025, 1, 1, ".json")]
        [InlineData($"logFile-20251202.json", true, "logFile-", 2025, 12, 2, ".json")]
        [InlineData($"logFile-20250324.json", true, "logFile-", 2025, 3, 24, ".json")]
        [InlineData($"logFile-.json", false, "logFile-", null, null, null, ".json")]
        public void GetLogFileDate(string fileName, bool expectedIsValid, 
            string expectedFilePrefix, int? year, int? month, int? day, string expectedFileExtension)
        {
            bool isValid = true;
            string filePrefix = string.Empty;
            DateTime? logFileDate = null;
            string fileExtension = string.Empty;
            (isValid, filePrefix, logFileDate, fileExtension) = LogSearch.GetLogFileDate(fileName);
            Assert.Equal(expectedIsValid, isValid);
            if (isValid)
            {
                Assert.Equal(expectedFilePrefix, filePrefix);
                if (year != null && month != null && day != null)
                {
                    Assert.Equal(new DateTime(year.Value, month.Value, day.Value), logFileDate);
                }
                else
                {
                    Assert.Null(logFileDate);
                }
                Assert.Equal(expectedFileExtension, fileExtension);
            }
            else
            {
                Assert.Equal(string.Empty, filePrefix);
                Assert.Null(logFileDate);
                Assert.Equal(string.Empty, fileExtension);
            }
        }

        ~LogSearchTests()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                if(_scratchDirectory != null)
                {
                    _scratchDirectory.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
