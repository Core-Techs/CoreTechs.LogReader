using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreTechs.LogReader
{
    public class LogFile
    {
        public FileInfo File { get; set; }

        public LogFile(FileInfo file)
        {
            File = file;
        }

        public IEnumerable<LogFileLine> ReadLogFileLines()
        {
            using (var tmp = new TempFile())
            {
                StreamReader reader;

                try
                {
                    var stream = File.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    reader = new StreamReader(stream);
                }
                catch (IOException)
                {
                    File.CopyTo(tmp.File.FullName);
                    reader = new StreamReader(tmp.File.OpenRead());
                }

                using (reader)
                    foreach (var l in reader.ReadLines().Select((l, i) =>
                        new LogFileLine
                        {
                            Text = l,
                            LineNumber = i + 1,
                            LogFile = this
                        }))
                        yield return l;
            }
        }

        public IEnumerable<LogEntry> ReadLogEntries()
        {
            var fields = GetFields();

            return ReadLogFileLines().Where(l => !l.Text.StartsWith("#"))
                .Select(e => new LogEntry(e, fields));
        }

        public string[] GetFields()
        {
            const string fields = "#Fields: ";
            return ReadLogFileLines().First(x => x.Text.StartsWith(fields)).Text.TrimStart(fields).Split();
        }
    }
}