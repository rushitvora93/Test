using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestProjekt.Helper
{
    public class TestConfiguration
    {
        private static TestConfiguration testConfiguration;

        public string AppSettingValue { get; set; }

        public static string GetAppSetting(string key)
        {
            testConfiguration = GetCurrentSettings(key);
            return testConfiguration.AppSettingValue;
        }

        private TestConfiguration(IConfiguration config, string key)
        {
            AppSettingValue = config.GetValue<string>(key);
        }

        // Get a valued stored in the appsettings.
        // Pass in a key like TestArea:TestKey to get TestValue
        public static TestConfiguration GetCurrentSettings(string Key)
        {
            var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var settings = new TestConfiguration(configuration, Key);

            return settings;
        }

        // IP:Port auf dem der WinAppDriver läuft
        public static string GetWindowsApplicationDriverUrl()
        {
            return GetAppSetting("WindowsApplicationDriverUrl");
        }
        //relativer Pfad zum WinAppDriver
        public static string RelWinAppDriverPath()
        {
            return GetAppSetting("RelWinAppDriverPath");
        }
        //Pfad oder AppId UWP der zu Testenden Anwendung
        public static string WpfAppId()
        {
            return GetAppSetting("StartUpPath");
        }
        //Pfad zum Working directory
        public static string WorkingDir()
        {
            return GetAppSetting("WorkingDir");
        }
        public static string UnlockToolPath()
        {
            return GetAppSetting("UnlockToolPath");
        }
    }
}