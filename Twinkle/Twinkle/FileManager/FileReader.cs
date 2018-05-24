using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Twinkle.FileManager
{
    public class FileReader : IDisposable
    {
        public Stream Stream { get; }

        // Open an existing file
        public FileReader(string path, string name)
        {
            try
            {
                Stream = new FileStream(path + name, FileMode.Open);
            }
            catch (Exception)
            {
                // The file does not exist
                Stream = null;
            }
        }

        // Close stream
        public void Dispose()
        {
            Stream?.Close();
        }
    }
}
