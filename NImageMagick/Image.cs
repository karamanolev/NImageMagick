using System;
using System.Linq;

namespace NImageMagick
{
    public class Image : IDisposable
    {
        public IntPtr Handle { get; private set; }

        public int ImageCompressionQuality
        {
            get
            {
                return (int)ImageMagick.MagickGetImageCompressionQuality(this.Handle);
            }
            set
            {
                this.ExecuteChecked(ImageMagick.MagickSetImageCompressionQuality, value);
            }
        }

        public int Width
        {
            get
            {
                return (int)ImageMagick.MagickGetImageWidth(this.Handle);
            }
        }

        public int Height
        {
            get
            {
                return (int)ImageMagick.MagickGetImageHeight(this.Handle);
            }
        }

        public Image(string path)
        {
            ImageMagick.EnsureInitialized();
            Handle = ImageMagick.NewMagickWand();
            if (Handle == IntPtr.Zero)
            {
                throw new Exception("Error acquiring wand.");
            }

            using (NativeString pathString = new NativeString(path))
            {
                this.ExecuteChecked(ImageMagick.MagickReadImage, pathString.Pointer);
            }
        }

        public void ExecuteChecked<T>(Func<IntPtr, T, int> action, T param1)
        {
            if (action(this.Handle, param1) != 1)
            {
                throw new ImageMagickException(this.Handle);
            }
        }

        public void ExecuteChecked<T1, T2>(Func<IntPtr, T1, T2, int> action, T1 param1, T2 param2)
        {
            if (action(this.Handle, param1, param2) != 1)
            {
                throw new ImageMagickException(this.Handle);
            }
        }

        public void ExecuteChecked<T1, T2, T3, T4>(Func<IntPtr, T1, T2, T3, T4, int> action, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (action(this.Handle, param1, param2, param3, param4) != 1)
            {
                throw new ImageMagickException(this.Handle);
            }
        }

        public void Save(string path)
        {
            using (NativeString pathString = new NativeString(path))
            {
                this.ExecuteChecked(ImageMagick.MagickWriteImage, pathString.Pointer);
            }
        }

        public void Resize(int width, int height, FilterType filterType, double blur)
        {
            this.ExecuteChecked(ImageMagick.MagickResizeImage, width, height, (int)filterType, blur);
        }

        public void Crop(int width, int height, int x, int y)
        {
            this.ExecuteChecked(ImageMagick.MagickCropImage, width, height, x, y);
        }

        public void GaussianBlur(double radius, double sigma)
        {
            this.ExecuteChecked(ImageMagick.MagickGaussianBlurImage, radius, sigma);
        }

        public void MagickUnsharpMaskImage(double radius, double sigma, double amount, double threshold)
        {
            this.ExecuteChecked(ImageMagick.MagickUnsharpMaskImage, radius, sigma, amount, threshold);
        }

        public void Dispose()
        {
            this.Handle = ImageMagick.DestroyMagickWand(this.Handle);
        }
    }
}
