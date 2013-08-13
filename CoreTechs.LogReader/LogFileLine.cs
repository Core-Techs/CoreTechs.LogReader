namespace CoreTechs.LogReader
{
    public class LogFileLine
    {
        public string Text { get; set; }
        public int LineNumber { get; set; }
        public LogFile LogFile { get; set; }
    }
}