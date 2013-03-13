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

            // test split
            Image image = new Image(@"C:\Temp\planet-hoth-wampas.jpg");
            Image[] parts = image.SplitH(100, 150);
            parts[0].Write(@"C:\Temp\temp1.png");
            parts[1].Write(@"C:\Temp\temp2.png");
            parts[2].Write(@"C:\Temp\temp3.png");

            // test tranparent
            MagickPixelWand target = new MagickPixelWand();
            target.Color = "#ffffff";
            image.Transparent(target, 0.0, 3276.8, false);
            image.Write(@"C:\Temp\temp4.png");

            // test colorspace
            image.Colorspace(ColorspaceType.GRAYColorspace);
            image.Write(@"C:\Temp\temp5.png");

            // test threshold
            image.Threshold(32768.0);
            image.Write(@"C:\Temp\temp6.png");

            // test fill
            target = new MagickPixelWand();
            target.Color = "#000000";
            MagickPixelWand fill = new MagickPixelWand();
            fill.Color = "#267F00";
            image.Fill(target, fill, 3276.8, false);
            image.Write(@"C:\Temp\temp7.png");
        }
    }
}
