
using Client.UseCases.UseCases;
using Core.UseCases;
using Microsoft.Win32;
using System;
using System.Globalization;

namespace FrameworksAndDrivers.Data.Registry
{
    public class RegistryDataAccess : ILanguageData, ISessionInformationRegistryDataAccess
    {

        public string GetLastLanguage()
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\QST", true);
            if (key == null)
            {
                RegistryKey defaultKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\QST");
                var defaultKeyValue = "en-US";
                defaultKey.SetValue("language", defaultKeyValue);
                return defaultKey.GetValue("language").ToString();
            }
            else
            {
                return key.GetValue("language").ToString();
            }
        }

        public void SetDefaultLanguage(string language)
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\QST", true);
            if (key == null)
            {
                RegistryKey defaultKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\QST");
                defaultKey.SetValue("language", language);

            }
            else
            {
                key.SetValue("language", language);
            }
        }

        public void SetMegaMainMenuIsPinned(bool isPinned)
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\QST", true);
            if (key == null)
            {
                RegistryKey defaultKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\QST");
                defaultKey.SetValue("FixedMainMenu", isPinned);

            }
            else
            {
                key.SetValue("FixedMainMenu", isPinned);
            }
        }

        public bool LoadMegaMainMenuIsPinned()
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\QST", true);
            if (key == null)
            {
                RegistryKey defaultKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\QST");
                bool isLocked = true;
                defaultKey.SetValue("FixedMainMenu", isLocked);
                object keyValue = defaultKey.GetValue("FixedMainMenu");
                return Convert.ToBoolean(keyValue, CultureInfo.InvariantCulture);
            }
            else
            {
                return Convert.ToBoolean(key.GetValue("FixedMainMenu"), CultureInfo.InvariantCulture);
            }
        }
    }
}
