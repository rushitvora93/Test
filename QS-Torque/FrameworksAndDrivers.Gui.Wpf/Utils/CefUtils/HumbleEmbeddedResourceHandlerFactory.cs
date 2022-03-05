using CefSharp;

namespace FrameworksAndDrivers.Gui.Wpf.CefUtils
{
    public class HumbleEmbeddedResourceHandlerFactory : ISchemeHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            return new HumbleEmbeddedResourceHandler();
        }        
    }
}
