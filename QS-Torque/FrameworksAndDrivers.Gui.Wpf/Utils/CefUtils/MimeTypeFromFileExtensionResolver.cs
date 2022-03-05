using System.IO;

namespace FrameworksAndDrivers.Gui.Wpf.CefUtils
{
    public class MimeTypeFromFileExtensionResolver
    {
        public static string GetMimeTypeFromFileExtension(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".html":
                    return "text/html";
                case ".js":
                    return "text/javascript";
                case ".png":
                    return "image/png";
                case ".appcache":
                case ".manifest":
                    return "text/cache-manifest";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
