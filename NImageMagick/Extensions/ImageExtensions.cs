using System;
using System.Collections.Generic;
using System.Linq;

namespace NImageMagick.Extensions
{
    public static class ImageExtensions
    {
        public static void Fit(this Image image, int size)
        {
            image.Fit(size, size);
        }

        public static void Fit(this Image image, int width, int height)
        {
            double ratio = Math.Min((double)width / image.Width, (double)height / image.Height);
            int newWidth = (int)Math.Round(image.Width * ratio);
            int newHeight = (int)Math.Round(image.Height * ratio);
            image.Resize(newWidth, newHeight, FilterType.CubicFilter, 1);
        }

        private static int[] SplitHEqual(this Image image, int parts)
        {
            List<int> splits = new List<int>();

            for (int i = 1; i < parts; ++i)
            {
                int split = (int)Math.Round((double)i / parts * image.Width);
                splits.Add(split);
            }

            return splits.ToArray();
        }

        public static Image[] SplitH(this Image image, params int[] parts)
        {
            if (parts.Length == 1 && parts[0] < 10)
            {
                parts = SplitHEqual(image, parts[0]);
            }

            List<Image> images = new List<Image>();
            int prev = 0;
            foreach (int split in parts)
            {
                Image crop = new Image(image);
                crop.Crop(split - prev, image.Height, prev, 0);
                images.Add(crop);
                prev = split;
            }
            Image lastCrop = new Image(image);
            lastCrop.Crop(image.Width - prev, image.Height, prev, 0);
            images.Add(lastCrop);
            return images.ToArray();
        }
    }
}
