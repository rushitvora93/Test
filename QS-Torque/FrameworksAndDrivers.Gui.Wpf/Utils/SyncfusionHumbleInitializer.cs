using Syncfusion.Licensing;

namespace FrameworksAndDrivers.Gui.Wpf
{
    /// <summary>
    /// Static intializier for the 3rd party Syncfusion WPF libraries
    /// </summary>
    public static class SyncfusionHumbleInitializer
    {
        /// <summary>
        /// License key gathered at the syncfusion website (Account information: admins@csp-sw.de).
        /// </summary>
        private const string LicenseKey = "NDM1MjU5QDMxMzcyZTMzMmUzMFBpbXQ0bUdzNmJqSjFhdnFmVENZYVdPb3JHanBERVh5QUNOa1p0NFJtcEU9";

        /// <summary>
        /// Registers the license for the syncfusion libraries
        /// </summary>
        public static void InitSyncfusion()
        {
            SyncfusionLicenseProvider.RegisterLicense(LicenseKey);
        }
    }
}
