using System;
using System.Linq;
using NImageMagick;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var image = new Image(@"Z:\Temp\dcd\folder.jpg");
            image.Crop(200, 200, 200, 200);
            image.Resize(800, 600, FilterType.CubicFilter, 1);
            image.Save(@"C:\Temp\test.jpg");
        }
    }
}
