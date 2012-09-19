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
            image.Crop(400, 390, 0, 10);
            image.Write("C:\\Temp\\images\\temp.jpg");
        }
    }
}
