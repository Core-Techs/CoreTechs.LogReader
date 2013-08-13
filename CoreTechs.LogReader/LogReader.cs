using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreTechs.LogReader
{
    public class LogReader
    {
        public string Path { get; set; }
        public string LogFileSearchPattern { get; set; }
        public SearchOption? LogFileSearchOption { get; set; }

        public LogReader(string path)
        {
            Path = path;
        }

        public IEnumerable<LogFile> GetLogFiles()
        {
            var fi = new FileInfo(Path);
            if (fi.Exists)
                yield return new LogFile(fi);

            var di = new DirectoryInfo(Path);
            if (di.Exists)
            {
                var files = di.EnumerateFiles(LogFileSearchPattern ?? "*.*", LogFileSearchOption ?? SearchOption.AllDirectories);
                foreach (var lf in files.Select(d => new LogFile(d)))
                    yield return lf;
            }
        }

        public IEnumerable<LogEntry> ReadLogEntries(Func<LogFile, bool> logFilePredicate = null)
        {
            return (logFilePredicate == null
                ? GetLogFiles()
                : GetLogFiles().Where(logFilePredicate))
                .SelectMany(lf => lf.ReadLogEntries());
        }

        public string[] GetFields()
        {
            return GetLogFiles().SelectMany(f => f.GetFields()).Distinct().ToArray();
        }
    }
}