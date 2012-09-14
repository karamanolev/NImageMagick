using System;
using System.Collections.Generic;
using System.Linq;

namespace NImageMagick.Extensions
{
    public static class ImageExtensions
    {
        public static void Fit(this Image image, int width, int height)
        {
            double ratio = Math.Min((double)width / image.Width, (double)height / image.Height);
            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);
            image.Resize(newWidth, newHeight, FilterType.CubicFilter, 1);
        }

        public static Image[] SplitH(this Image image, int parts)
        {
            List<Image> images = new List<Image>();

            int start = 0;
            for (int i = 1; i <= parts; ++i)
            {
                int end = (int)Math.Round((double)i / parts * image.Width);
                Image newImage = new Image(image);
                newImage.Crop(end - start, image.Height, start, 0);
                images.Add(newImage);
                start = end;
            }

            return images.ToArray();
        }
    }
}
