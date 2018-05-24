using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Twinkle.FileManager
{
    public class FileWriter : IDisposable
    {
        public StreamWriter Stream { get; }

        public FileWriter(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Stream = File.CreateText(String.Format("{0}/{1}", path, name));
        }

        public void Dispose()
        {
            Stream.Close();
        }
    }
}
