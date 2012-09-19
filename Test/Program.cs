using System;
using System.Linq;
using NImageMagick;
using NImageMagick.Extensions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryLoader.LoadLibraries();

            Image image = new Image(@"C:\Temp\images\CD's.jpg");
            image.Crop(400, 400, 100, 100);
            image.Crop(400, 400, 0, 0);
            image.Write("C:\\Temp\\images\\temp.jpg");
        }
    }
}
