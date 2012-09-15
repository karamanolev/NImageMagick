NImageMagick
============

.NET wrapper for ImageMagick

NImageMagick is just a wrapper around the ImageMagick DLLs. Ways to obtain DLLs for ImageMagick itself:
- Use the ImageMagick installer on every machine where your software is deployed.
- Just copy all dlls from the NImageMagick\Test\Libraries folder to your project and make sure they ship in your application directory (or use the LibraryLoader from Test to use them from a different folder).
- Build the ImageMagick sources, making sure registry paths are NOT used (the default is not to use them).