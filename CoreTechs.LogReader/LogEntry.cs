using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreTechs.LogReader
{
    public class LogEntry : Dictionary<string, string>
    {
        public LogFile LogFile { get { return Line.LogFile; } }
        public LogFileLine Line { get; private set; }
        public DateTime DateTime { get { return DateTime.Parse("{0} {1}".Fmt(this["date"], this["time"])); } }

        public LogEntry(LogFileLine logFileLine, IEnumerable<string> fields)
            : base(ConstructDictionary(logFileLine, fields))
        {
            Line = logFileLine;
        }

        static IDictionary<string, string> ConstructDictionary(LogFileLine line, IEnumerable<string> fields)
        {
            return fields.Zip(line.Text.Split(), Tuple.Create).ToDictionary(f => f.Item1, f => f.Item2, StringComparer.OrdinalIgnoreCase);
        }

        public dynamic AsDynamic()
        {
            return new DynamicLogEntry(this);
        }
    }
}