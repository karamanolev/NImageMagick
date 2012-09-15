using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NImageMagick
{
    static class ImageMagick
    {
        internal const string WandDll = "CORE_RL_Wand_.dll";
        internal const CallingConvention WandConvention = CallingConvention.Cdecl;

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void MagickWandGenesis();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void MagickWandTerminus();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr NewMagickWand();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickReadImage(IntPtr image, IntPtr path);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickWriteImage(IntPtr image, IntPtr path);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr DestroyMagickWand(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetException(IntPtr ptr, out int exceptionType);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickClearException(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickGetImageCompressionQuality(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickSetImageCompressionQuality(IntPtr ptr, int quality);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickGetImageWidth(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickGetImageHeight(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickResizeImage(IntPtr ptr, int columns, int rows, int filterType, double blur);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickCropImage(IntPtr ptr, int width, int height, int x, int y);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickGaussianBlurImage(IntPtr ptr, double radius, double sigma);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickUnsharpMaskImage(IntPtr ptr, double radius, double sigma, double amount, double threshold);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetImageFilename(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetImageFormat(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickSetImageFormat(IntPtr ptr, IntPtr format);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr CloneMagickWand(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetImageBlob(IntPtr ptr, out int length);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickReadImageBlob(IntPtr ptr, IntPtr blob, int length);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickRelinquishMemory(IntPtr resource);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetQuantumDepth(out int depth);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr MagickGetVersion(out int version);

        public static string BinPath { get; set; }
        public static string CodersModulePath { get; set; }
        public static string FiltersModulePath { get; set; }

        static ImageMagick()
        {
            BinPath = ".";
            CodersModulePath = ".";
            FiltersModulePath = ".";
        }

        private static object isInitializedSyncRoot = new object();
        private static bool isInitialized = false;
        internal static void EnsureInitialized()
        {
            if (!isInitialized)
            {
                lock (isInitializedSyncRoot)
                {
                    if (!isInitialized)
                    {
                        MagickWandGenesis();
                        isInitializedSyncRoot = true;
                    }
                }
            }
        }

        internal static string ExtractVersionNumber()
        {
            int version;
            MagickGetVersion(out version);
            return string.Join(".", version.ToString("x").ToArray());
        }

        internal static void FreeString(IntPtr str)
        {
            Marshal.FreeHGlobal(str);
        }
    }
}
