using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Demo.src
{
    public class FileAndDir
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
