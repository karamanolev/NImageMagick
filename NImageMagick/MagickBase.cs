using System;
using System.Linq;

namespace NImageMagick
{
    public class MagickBase
    {
        public IntPtr Handle { get; protected set; }

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

        public void ExecuteChecked<T1, T2, T3>(Func<IntPtr, T1, T2, T3, int> action, T1 param1, T2 param2, T3 param3)
        {
            if (action(this.Handle, param1, param2, param3) != 1)
            {
                throw new ImageMagickException(this.Handle);
            }
        }
    }
}
