using System;
using System.Linq;
using NImageMagick;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryLoader.LoadLibraries();

            Image image = new Image(500, 500, new MagickPixelWand()
            {
                Color = "none"
            });
            image.Write(@"C:\Temp\temp.png");
        }
    }
}
