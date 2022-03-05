using FrameworksAndDrivers.Gui.Wpf.CefUtils;
using NUnit.Framework;

namespace FrameworksAndDrivers.Gui.Wpf.Test.CefUtils
{
    public class MimeTypeFromFileExtensionResolverTest
    {
        [TestCase(".png", "image/png")]
        [TestCase(".html", "text/html")]
        [TestCase(".js", "text/javascript")]
        [TestCase(".appcache", "text/cache-manifest")]
        [TestCase(".manifest", "text/cache-manifest")]
        [TestCase(".blub2000", "application/octet-stream")]
        public void GettingMimeTypeFromFileExtensionReturnsCorrectResult(string filetype, string expectedResult)
        {
            Assert.AreEqual(expectedResult, MimeTypeFromFileExtensionResolver.GetMimeTypeFromFileExtension(filetype));
        }
    }
}
