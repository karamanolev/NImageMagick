using System;
using System.Linq;
using System.Runtime.InteropServices;

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
            int exceptionSeverity = 0;
            IntPtr exceptionPtr = ImageMagick.MagickGetException(wandHandle, ref exceptionSeverity);
            return Marshal.PtrToStringAnsi(exceptionPtr);
        }
    }
}
