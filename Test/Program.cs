using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using Microsoft.Win32;
using NImageMagick;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryLoader.LoadLibraries();

            Image image = new Image(@"C:\Temp\images\CD.png");
        }
    }
}
