using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreTechs.LogReader
{
    public static class Extensions
    {
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string TrimStart(this string s, string trimString)
        {
            return s.StartsWith(trimString)
                ? s.Remove(0, trimString.Length)
                : s;
        }

        public static IEnumerable<string> ReadLines(this StreamReader reader)
        {
            string l;
            while ((l = reader.ReadLine()) != null)
                yield return l;
        }

        public static string AsString(this IEnumerable<char> chars)
        {
            return new string(chars.ToArray());
        }

        public static IEnumerable<dynamic> AsDynamic(this IEnumerable<LogEntry> logEntries)
        {
            return logEntries.Select(e => new DynamicLogEntry(e));
        }
    }
}
