NImageMagick
============

.NET wrapper for ImageMagick

NImageMagick is just a wrapper around the ImageMagick DLLs. Ways to obtain DLLs for ImageMagick itself:
- Use the ImageMagick installer on every machine where your software is deployed.
- Just copy all dlls from the NImageMagick\Test\Libraries folder to your project and make sure they ship in your application directory (or use the LibraryLoader from Test to use them from a different folder).
- Build the ImageMagick sources, making sure registry paths are NOT used (the default is not to use them).

Building ImageMagick from sources:
- Download the latest source code archive (make sure you download the windows version of the sources, that include VisualMagick)
- Build the configure project (build it in Release mode, otherwise it crashes)
- I recommend to keep the default settings of configure, they work
- Build All.sln
- Use CORE_RL_*.dll and IM_MOD_*.dll, X11.dll