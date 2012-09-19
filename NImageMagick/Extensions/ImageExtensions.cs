using System;
using System.Collections.Generic;
using System.Linq;

namespace NImageMagick.Extensions
{
    public static class ImageExtensions
    {
        public static IEnumerable<Image> Crop(this IEnumerable<Image> images, int width, int height, int x, int y)
        {
            foreach (Image image in images)
            {
                image.Crop(width, height, x, y);
                yield return image;
            }
        }

        public static IEnumerable<Image> Fit(this IEnumerable<Image> images, int width, int height)
        {
            foreach (Image image in images)
            {
                image.Fit(width, height);
                yield return image;
            }
        }

        public static IEnumerable<Image> Fit(this IEnumerable<Image> images, int size)
        {
            foreach (Image image in images)
            {
                image.Fit(size);
                yield return image;
            }
        }

        public static IEnumerable<Image> Resize(this IEnumerable<Image> images, int width, int height)
        {
            foreach (Image image in images)
            {
                image.Resize(width, height, FilterType.LanczosSharpFilter, 1);
                yield return image;
            }
        }
    }
}
