using System;
using System.IO;
using System.Linq;
using NImageMagick;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryLoader.LoadLibraries();

            Image image = new Image(@"C:\Temp\Images\CD.png");
        }
    }
}
