using System;
using System.IO;

namespace CoreTechs.LogReader
{
    public class TempFile : IDisposable
    {
        public FileInfo File { get; private set; }

        public TempFile()
        {
            var tmp = Path.GetTempFileName();
            File = new FileInfo(tmp);
        }

        public void Dispose()
        {
            if (File.Exists)
                File.Delete();
        }
    }
}