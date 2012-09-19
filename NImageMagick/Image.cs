using System;
using System.Collections.Generic;
using System.Linq;

namespace NImageMagick
{
    public class Image : IDisposable
    {
        private MagickImage image;

        public int CompressionQuality
        {
            get { return this.image.ImageCompressionQuality; }
            set { this.image.ImageCompressionQuality = value; }
        }

        public int Width
        {
            get { return this.image.Width; }
        }

        public int Height
        {
            get { return this.image.Height; }
        }

        public string Filename
        {
            get { return this.image.Filename; }
        }

        public string Format
        {
            get { return this.image.Format; }
            set { this.image.Format = value; }
        }

        public Image(string path)
        {
            this.image = new MagickImage(path);
        }

        public Image(byte[] blob)
        {
            this.image = new MagickImage(blob);
        }

        public Image(Image image)
        {
            this.image = new MagickImage(image.image);
        }

        public Image(MagickImage image)
        {
            this.image = image;
        }

        public void Write(string path)
        {
            this.image.Write(path);
        } 

        public void Resize(int width, int height, FilterType filterType = FilterType.LanczosSharpFilter, double blur = 1)
        {
            this.image.Resize(width, height, filterType, blur);
        }

        public void Crop(int width, int height, int x, int y)
        {
            if (width <= 0)
            {
                width = this.Width - x - width;
            }
            if (height <= 0)
            {
                height = this.Height - y - height;
            }
            this.image.Crop(width, height, x, y);
            this.image.ResetImagePage("0x0+0+0");
        }

        public void GaussianBlur(double radius, double sigma)
        {
            this.image.GaussianBlur(radius, sigma);
        }

        public void UnsharpMask(double radius, double sigma, double amount, double threshold)
        {
            this.image.UnsharpMaskImage(radius, sigma, amount, threshold);
        }

        public byte[] GetBlob()
        {
            return this.image.GetBlob();
        }

        public void Dispose()
        {
            this.image.Dispose();
            this.image = null;
        }

        public void Fit(int size)
        {
            this.Fit(size, size);
        }

        public void Fit(int width, int height)
        {
            double ratio = Math.Min((double)width / this.Width, (double)height / this.Height);
            int newWidth = (int)Math.Round(this.Width * ratio);
            int newHeight = (int)Math.Round(this.Height * ratio);
            this.Resize(newWidth, newHeight, FilterType.CubicFilter, 1);
        }

        private static int[] GetSplitEqualPoints(int size, int parts)
        {
            List<int> splits = new List<int>();

            for (int i = 1; i < parts; ++i)
            {
                int split = (int)Math.Round((double)i / parts * size);
                splits.Add(split);
            }

            return splits.ToArray();
        }

        public Image[] SplitH(params int[] parts)
        {
            if (parts.Length == 1 && parts[0] < 10)
            {
                parts = GetSplitEqualPoints(this.Width, parts[0]);
            }

            List<Image> images = new List<Image>();
            int prev = 0;
            foreach (int split in parts)
            {
                Image crop = new Image(image);
                crop.Crop(split - prev, this.Height, prev, 0);
                images.Add(crop);
                prev = split;
            }
            Image lastCrop = new Image(image);
            lastCrop.Crop(this.Width - prev, this.Height, prev, 0);
            images.Add(lastCrop);
            return images.ToArray();
        }
    }
}
