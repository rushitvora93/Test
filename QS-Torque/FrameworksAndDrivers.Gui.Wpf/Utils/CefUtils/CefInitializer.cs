using System;
using System.IO;
using System.Reflection;
using CefSharp;
using CefSharp.WinForms;

namespace FrameworksAndDrivers.Gui.Wpf.CefUtils
{
    public static class CefHumbleInitializer
    {
        public static void InitCef()
        {
            CefSettings settings = new CefSettings();
            settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                Environment.Is64BitProcess ? "x64" : "x86",
                "CefSharp.BrowserSubprocess.exe");
            settings.RegisterScheme(new CefSharp.CefCustomScheme() { SchemeName = "embedded", SchemeHandlerFactory = new HumbleEmbeddedResourceHandlerFactory() });
            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
        }

        public static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    Environment.Is64BitProcess ? "x64" : "x86",
                    assemblyName);

                return File.Exists(archSpecificPath)
                    ? Assembly.LoadFile(archSpecificPath)
                    : null;
            }

            return null;
        }
    }
}
