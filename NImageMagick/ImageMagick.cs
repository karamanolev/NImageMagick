﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NImageMagick
{
    static class ImageMagick
    {
        public const string WandDll = "CORE_RL_Wand_.dll";
        public const CallingConvention WandConvention = CallingConvention.Cdecl;

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern void MagickWandGenesis();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern void MagickWandTerminus();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern IntPtr NewMagickWand();

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern int MagickReadImage(IntPtr image, IntPtr path);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern int MagickWriteImage(IntPtr image, IntPtr path);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern IntPtr DestroyMagickWand(IntPtr ptr);

        //[DllImport(WandDll, CallingConvention = WandConvention)]
        //public static extern int MagickGetExceptionType(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern IntPtr MagickGetException(IntPtr ptr, out int exceptionType);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern int MagickClearException(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern uint MagickGetImageCompressionQuality(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern int MagickSetImageCompressionQuality(IntPtr ptr, uint quality);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern uint MagickGetImageWidth(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern uint MagickGetImageHeight(IntPtr ptr);

        [DllImport(WandDll, CallingConvention = WandConvention)]
        public static extern int MagickResizeImage(IntPtr ptr, uint columns, uint rows, int filterType, double blur);

        private static object isInitializedSyncRoot = new object();
        private static bool isInitialized = false;
        public static void EnsureInitialized()
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

        public static void FreeString(IntPtr str)
        {
            Marshal.FreeHGlobal(str);
        }
    }
}