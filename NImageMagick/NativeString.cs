using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NImageMagick
{
    class NativeString : IDisposable
    {
        private static byte[] Single0ByteArray = new byte[] { 0 };

        public IntPtr Pointer { get; private set; }

        public NativeString(string value)
        {
            byte[] utf8 = Encoding.UTF8.GetBytes(value);
            this.Pointer = Marshal.AllocHGlobal(utf8.Length + 1);
            Marshal.Copy(utf8, 0, this.Pointer, utf8.Length);
            Marshal.Copy(Single0ByteArray, 0, this.Pointer + utf8.Length, 1);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(this.Pointer);
        }
    }
}
