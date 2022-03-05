using System;
using System.Collections.Generic;
using System.Windows;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using FrameworksAndDrivers.Localization;
using ServerConnections.Gui;
using State;

namespace ServerConnections
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var serverConnectionStorage = new DataAccess.ServerConnectionStorageXml(new DataAccess.CreateFileStream(), FilePaths.ServerConnectionsConfigPath);
            var useCaseFactory = new UseCases.ServerConnectionUseCaseFactory(serverConnectionStorage, new DataAccess.ServerConnectionChecker());

            var localization = new LocalizationWrapper("Messages");
            localization.SetLanguage("de-DE");

            var themeUris = new Dictionary<Theme, Uri>
            {
                { Theme.Normal, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/QstColors.xaml", UriKind.RelativeOrAbsolute) },
                { Theme.Dark, new Uri("/FrameworksAndDrivers.Gui.Wpf;component/View/Themes/DarkModeColors.xaml", UriKind.RelativeOrAbsolute) }
            };

            var themeDict = new ThemeDictionary(themeUris);

            this.Resources.Add(ResourceKeys.ApplicationThemeDictionaryKey, themeDict);
            this.Resources.MergedDictionaries.Add(themeDict);

            if (FeatureToggles.FeatureToggles.CommandLineInterfaceForServerConnection && e.Args.Length > 0)
            {
                var ConsoleView = new ServerConnectionConsoleView(useCaseFactory, localization, e.Args, new HumbleConsoleWriter());
                this.Shutdown(ConsoleView.Execute());
            }
            else
            {
                MainWindow = new Gui.ServerConnectionView(useCaseFactory, localization);
                MainWindow.Show();
            }
        }
    }
}
