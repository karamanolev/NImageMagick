using System;
using System.Linq;
using Quantum = System.UInt16;

namespace NImageMagick
{
    public class MagickPixelWand : MagickBase, IDisposable
    {
        public double Alpha
        {
            get { return ImageMagick.PixelGetAlpha(this.Handle); }
            set { ImageMagick.PixelSetAlpha(this.Handle, value); }
        }

        public double Opacity
        {
            get { return ImageMagick.PixelGetOpacity(this.Handle); }
            set { ImageMagick.PixelSetOpacity(this.Handle, value); }
        }

        public double Red
        {
            get { return ImageMagick.PixelGetRed(this.Handle); }
            set { ImageMagick.PixelSetRed(this.Handle, value); }
        }

        public double Green
        {
            get { return ImageMagick.PixelGetGreen(this.Handle); }
            set { ImageMagick.PixelSetGreen(this.Handle, value); }
        }

        public double Blue
        {
            get { return ImageMagick.PixelGetBlue(this.Handle); }
            set { ImageMagick.PixelSetBlue(this.Handle, value); }
        }

        public Quantum AlphaQuantum
        {
            get { return ImageMagick.PixelGetAlphaQuantum(this.Handle); }
            set { ImageMagick.PixelSetAlphaQuantum(this.Handle, value); }
        }

        public Quantum OpacityQuantum
        {
            get { return ImageMagick.PixelGetOpacityQuantum(this.Handle); }
            set { ImageMagick.PixelSetOpacityQuantum(this.Handle, value); }
        }

        public Quantum RedQuantum
        {
            get { return ImageMagick.PixelGetRedQuantum(this.Handle); }
            set { ImageMagick.PixelSetRedQuantum(this.Handle, value); }
        }

        public Quantum GreenQuantum
        {
            get { return ImageMagick.PixelGetGreenQuantum(this.Handle); }
            set { ImageMagick.PixelSetGreenQuantum(this.Handle, value); }
        }

        public Quantum BlueQuantum
        {
            get { return ImageMagick.PixelGetBlueQuantum(this.Handle); }
            set { ImageMagick.PixelSetBlueQuantum(this.Handle, value); }
        }

        public string Color
        {
            get
            {
                return NativeString.Load(ImageMagick.PixelGetColorAsString(this.Handle));
            }
            set
            {
                using (var colorString = new NativeString(value))
                {
                    this.ExecuteChecked(ImageMagick.PixelSetColor, colorString.Pointer);
                }
            }
        }

        public string NormalizedColor
        {
            get
            {
                return NativeString.Load(ImageMagick.PixelGetColorAsNormalizedString(this.Handle));
            }
        }

        public MagickPixelWand()
        {
            this.Handle = ImageMagick.NewPixelWand();
            if (this.Handle == IntPtr.Zero)
            {
                throw new Exception("Error acquiring pixel wand.");
            }
        }

        public MagickPixelWand(string sourceString)
            : this()
        {
            this.Color = sourceString;
        }

        ~MagickPixelWand()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Handle = ImageMagick.DestroyPixelWand(this.Handle);
        }

        public static MagickPixelWand FromRGB(double red, double green, double blue)
        {
            return new MagickPixelWand()
            {
                Red = red,
                Green = green,
                Blue = blue,
            };
        }

        public static MagickPixelWand FromARGB(double alpha, double red, double green, double blue)
        {
            return new MagickPixelWand()
            {
                Alpha = alpha,
                Red = red,
                Green = green,
                Blue = blue,
            };
        }
    }
}
