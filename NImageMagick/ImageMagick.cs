using System;
using System.Linq;
using System.Runtime.InteropServices;
using Quantum = System.UInt16;

namespace NImageMagick
{
    public static class ImageMagick
    {
        internal const string WandDll = "CORE_RL_Wand_.dll";
        internal const CallingConvention WandConvention = CallingConvention.Cdecl;

        #region Magick Wand

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void MagickWandGenesis();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void MagickWandTerminus();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr NewMagickWand();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickNewImage(IntPtr ptr, int columns, int rows, IntPtr background);

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
        internal static extern int MagickSetImageMatte(IntPtr ptr, int matte);

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

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickResetImagePage(IntPtr ptr, IntPtr page);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickCompositeImage(IntPtr ptr, IntPtr sourcePtr, int compositeOperator, int x, int y);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickRotateImage(IntPtr ptr, IntPtr background, double degrees);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickTransparentPaintImage(IntPtr wand, IntPtr target, double alpha, double fuzz, int invert);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickOpaquePaintImage(IntPtr wand, IntPtr target, IntPtr fill, double fuzz, int invert);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickThresholdImage(IntPtr wand, double threshold);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickAdaptiveThresholdImage(IntPtr wand, int width, int height, double bias);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickTransformImageColorspace(IntPtr wand, int colorspaceType);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickQuantizeImage(IntPtr wand, int number_colors, int colorsapceType, int treedepth, int dither_method, int measure_error);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int MagickNormalizeImage(IntPtr wand);

        #endregion

        #region Pixel Wand

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr NewPixelWand();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr DestroyPixelWand(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern int PixelSetColor(IntPtr wand, IntPtr color);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr PixelGetColorAsString(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern IntPtr PixelGetColorAsNormalizedString(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern double PixelGetAlpha(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetAlpha(IntPtr wand, double value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern double PixelGetOpacity(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetOpacity(IntPtr wand, double value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern double PixelGetRed(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetRed(IntPtr wand, double value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern double PixelGetGreen(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetGreen(IntPtr wand, double value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern double PixelGetBlue(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetBlue(IntPtr wand, double value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern Quantum PixelGetAlphaQuantum(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetAlphaQuantum(IntPtr wand, Quantum value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern Quantum PixelGetOpacityQuantum(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetOpacityQuantum(IntPtr wand, Quantum value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern Quantum PixelGetRedQuantum(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetRedQuantum(IntPtr wand, Quantum value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern Quantum PixelGetGreenQuantum(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetGreenQuantum(IntPtr wand, Quantum value);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern Quantum PixelGetBlueQuantum(IntPtr wand);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        internal static extern void PixelSetBlueQuantum(IntPtr wand, Quantum value);

        #endregion

        public static string VersionString
        {
            get
            {
                EnsureInitialized();

                int version;
                return NativeString.Load(MagickGetVersion(out version), false);
            }
        }

        public static int VersionNumber
        {
            get
            {
                EnsureInitialized();

                int version;
                MagickGetVersion(out version);
                return version;
            }
        }

        public static string VersionNumberString
        {
            get
            {
                return string.Join(".", VersionNumber.ToString("x").ToArray());
            }
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
    }
}
