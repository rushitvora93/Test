using System;
using System.IO;

namespace State
{
    public class FilePaths
    {
        public static string ServerConnectionsConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CSP/QS-Torque/ServerConnectionConfig.xml");

        public static string QstTempPath = Path.Combine(Path.GetTempPath(), "QS-Torque");
    }
}
