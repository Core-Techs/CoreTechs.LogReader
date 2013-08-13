using System;
using System.Dynamic;
using System.Linq;

namespace CoreTechs.LogReader
{
    public class DynamicLogEntry : DynamicObject
    {
        public LogEntry LogEntry { get; private set; }

        public DynamicLogEntry(LogEntry logEntry)
        {
            LogEntry = logEntry;
        }

        public override bool TryGetMember(GetMemberBinder binder, out Object result)
        {
            Func<string, string> strip = s => s.Where(char.IsLetterOrDigit).AsString().ToLowerInvariant();

            var key = LogEntry.Keys
                .SingleOrDefault(k =>
                    strip(k).Equals(strip(binder.Name), StringComparison.OrdinalIgnoreCase));

            result = key == null
                ? null
                : LogEntry[key];

            return true;
        }
    }
}