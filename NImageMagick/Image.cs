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

        public bool Matte
        {
            set { this.image.Matte = value; }
        }

        public Image(string path)
        {
            this.image = new MagickImage(path);
        }

        public Image(byte[] blob)
        {
            this.image = new MagickImage(blob);
        }

        /// <summary>
        /// Copies the image into a new one.
        /// </summary>
        public Image(Image image)
        {
            this.image = new MagickImage(image.image);
        }

        /// <summary>
        /// Creates a new Image instance using the same image as passed.
        /// </summary>
        public Image(MagickImage image)
        {
            this.image = image;
        }

        public Image(int width, int height, MagickPixelWand pixelWand)
        {
            this.image = new MagickImage(width, height, pixelWand);
        }

        private void ResetImagePage()
        {
            this.image.ResetImagePage("0x0+0+0");
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
            this.ResetImagePage();
        }

        public void GaussianBlur(double radius, double sigma)
        {
            this.image.GaussianBlur(radius, sigma);
        }

        public void MagickUnsharpMaskImage(double radius, double sigma, double amount, double threshold)
        {
            this.image.MagickUnsharpMaskImage(radius, sigma, amount, threshold);
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

        private static double[] GetSplitEqualPoints(int size, int parts)
        {
            List<double> splits = new List<double>();

            for (int i = 1; i < parts; ++i)
            {
                int split = (int)Math.Round((double)i / parts * size);
                splits.Add(split);
            }

            return splits.ToArray();
        }

        public Image[] SplitH(params double[] parts)
        {
            if (parts.Length == 1 && parts[0] > 1 && parts[0] < 10)
            {
                parts = GetSplitEqualPoints(this.Width, (int)parts[0]);
            }
            else if (parts.All(p => p > 0 && p < 1))
            {
                parts = parts.Select(p => p * this.Width).ToArray();
            }

            List<Image> images = new List<Image>();
            int prev = 0;
            foreach (double _split in parts)
            {
                int split = (int)Math.Round(_split);

                Image crop = new Image(this);
                crop.Crop(split - prev, this.Height, prev, 0);
                images.Add(crop);
                prev = split;
            }
            Image lastCrop = new Image(this);
            lastCrop.Crop(this.Width - prev, this.Height, prev, 0);
            images.Add(lastCrop);
            return images.ToArray();
        }

        public void Composite(Image sourceImage, int x, int y)
        {
            this.Composite(sourceImage, CompositeOperator.OverCompositeOp, x, y);
        }

        public void Composite(Image sourceImage, CompositeOperator compose, int x, int y)
        {
            this.image.CompositeImage(sourceImage.image, compose, x, y);
        }

        public void Rotate(double degrees)
        {
            this.Rotate(new MagickPixelWand(), degrees);
        }

        public void Rotate(MagickPixelWand background, double degrees)
        {
            this.image.Rotate(background, degrees);
            this.ResetImagePage();
        }

        public void Transparent(MagickPixelWand target, double alpha, double fuzz, bool invert)
        {
            this.image.Transparent(target, alpha, fuzz, invert);
        }

        public void Fill(MagickPixelWand target, MagickPixelWand fill, double fuzz, bool invert)
        {
            this.image.Fill(target, fill, fuzz, invert);
        }

        public void Threshold(double threshold)
        {
            this.image.Threshold(threshold);
        }

        public void Colorspace(ColorspaceType colorspaceType)
        {
            this.image.Colorspace(colorspaceType);
        }
    }
}
