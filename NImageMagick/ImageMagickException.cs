using System;
using System.Linq;

namespace NImageMagick
{
    public class ImageMagickException : Exception
    {
        public ImageMagickException(IntPtr wandHandle)
            : base(DecodeException(wandHandle))
        {
        }

        private static string DecodeException(IntPtr wandHandle)
        {
            int exceptionSeverity;
            IntPtr exceptionPtr = ImageMagick.MagickGetException(wandHandle, out exceptionSeverity);
            ImageMagick.MagickClearException(wandHandle);
            return NativeString.LoadAndRelinquish(exceptionPtr);
        }
    }
}
