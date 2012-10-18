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

            Image image = new Image(@"C:\Temp\Music Database\Images\1.jpg");
            Image[] parts = image.SplitH(100, 150);
            parts[0].Write(@"C:\Temp\temp1.png");
            parts[1].Write(@"C:\Temp\temp2.png");
            parts[2].Write(@"C:\Temp\temp3.png");
        }
    }
}
