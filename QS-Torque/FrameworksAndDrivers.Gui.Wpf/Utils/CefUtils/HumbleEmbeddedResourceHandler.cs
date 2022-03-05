using System;
using System.Threading.Tasks;
using CefSharp;
using System.Windows;
using System.Windows.Resources;
using log4net;

namespace FrameworksAndDrivers.Gui.Wpf.CefUtils
{
    public class HumbleEmbeddedResourceHandler : ResourceHandler
    {
        public override CefReturnValue ProcessRequestAsync(IRequest request, ICallback callback)
        {
            Uri u = new Uri(request.Url);
            string file = u.Authority;// + u.AbsolutePath;

            StreamResourceInfo stream = null;
            try
            {
                stream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources;component/Chromium/" + file));
            }
            catch (Exception e)
            {
                LogManager.GetLogger(typeof(HumbleEmbeddedResourceHandler)).Error("Resource " + file + " not found: " + e.Message);
            }

            if (stream != null)
            {
                Task.Run(() =>
                {
                    Stream = stream.Stream;
                    MimeType = GetMimeTypeFromFileExtension(file);
                    callback.Continue();
                });
                return CefReturnValue.ContinueAsync;
            }
            else
            {
                callback.Dispose();
                return CefReturnValue.Cancel;
            }
        }

        public string GetMimeTypeFromFileExtension(string file)
        {
            return MimeTypeFromFileExtensionResolver.GetMimeTypeFromFileExtension(file);
        }
    }
}
