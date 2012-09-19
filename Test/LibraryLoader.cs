using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Test
{
    static class LibraryLoader
    {
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        private static extern bool SetDllDirectory(string fileName);

        public static void LoadLibraries()
        {
            string path = Path.GetDirectoryName(typeof(LibraryLoader).Assembly.Location);
            path = Path.Combine(path, "Libraries\\x86");
            SetDllDirectory(path);
        }
    }
}
