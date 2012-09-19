using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NImageMagick
{
    public class MagickImage : IDisposable
    {
        public IntPtr Handle { get; private set; }

        public int ImageCompressionQuality
        {
            get
            {
                return ImageMagick.MagickGetImageCompressionQuality(this.Handle);
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
                return ImageMagick.MagickGetImageWidth(this.Handle);
            }
        }

        public int Height
        {
            get
            {
                return ImageMagick.MagickGetImageHeight(this.Handle);
            }
        }

        public string Filename
        {
            get
            {
                return NativeString.Load(ImageMagick.MagickGetImageFilename(this.Handle));
            }
        }

        public string Format
        {
            get
            {
                return NativeString.Load(ImageMagick.MagickGetImageFormat(this.Handle));
            }
            set
            {
                using (var formatString = new NativeString(value))
                {
                    this.ExecuteChecked(ImageMagick.MagickSetImageFormat, formatString.Pointer);
                }
            }
        }

        public MagickImage(string path)
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

        public MagickImage(byte[] blob)
        {
            ImageMagick.EnsureInitialized();

            Handle = ImageMagick.NewMagickWand();
            if (Handle == IntPtr.Zero)
            {
                throw new Exception("Error acquiring wand.");
            }

            IntPtr memory = Marshal.AllocHGlobal(blob.Length);
            try
            {
                Marshal.Copy(blob, 0, memory, blob.Length);
                this.ExecuteChecked(ImageMagick.MagickReadImageBlob, memory, blob.Length);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        public MagickImage(MagickImage image)
        {
            ImageMagick.EnsureInitialized();

            Handle = ImageMagick.CloneMagickWand(image.Handle);
            if (Handle == IntPtr.Zero)
            {
                throw new ImageMagickException(image.Handle);
            }
        }

        ~MagickImage()
        {
            this.Dispose();
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

        public void Write(string path)
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

        public void UnsharpMaskImage(double radius, double sigma, double amount, double threshold)
        {
            this.ExecuteChecked(ImageMagick.MagickUnsharpMaskImage, radius, sigma, amount, threshold);
        }

        public void ResetImagePage(string page)
        {
            using (var pageString = new NativeString(page))
            {
                this.ExecuteChecked(ImageMagick.MagickResetImagePage, pageString.Pointer);
            }
        }

        public byte[] GetBlob()
        {
            int length;
            IntPtr blobPointer = ImageMagick.MagickGetImageBlob(this.Handle, out length);
            if (blobPointer == IntPtr.Zero)
            {
                throw new ImageMagickException(this.Handle);
            }
            try
            {
                byte[] result = new byte[length];
                Marshal.Copy(blobPointer, result, 0, length);
                return result;
            }
            finally
            {
                ImageMagick.MagickRelinquishMemory(blobPointer);
            }
        }

        public void Dispose()
        {
            this.Handle = ImageMagick.DestroyMagickWand(this.Handle);
        }
    }
}
