using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NImageMagick
{
    public class Image
    {
        private IntPtr handle;

        public IntPtr Handle
        {
            get { return this.handle; }
        }

        public Image(string path)
        {
            ImageMagick.EnsureInitialized();
            handle = ImageMagick.NewMagickWand();
            if (handle == IntPtr.Zero)
            {
                throw new Exception("Error acquiring wand.");
            }
            IntPtr str = ImageMagick.AllocateUTF8String(path);
            int result = ImageMagick.MagickReadImage(this.handle, str);
            ImageMagick.FreeString(str);

            if (result == 0)
            {
                throw new ImageMagickException(this.handle);
            }
        }
    }
}
