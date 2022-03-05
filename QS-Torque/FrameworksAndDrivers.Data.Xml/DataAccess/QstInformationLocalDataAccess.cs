using Core.UseCases;
using State;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace FrameworksAndDrivers.Data.Xml.DataAccess
{
    public class QstInformationLocalDataAccess : IQstInformationDataLocal
    {
        private const string LogFolderName = "logs";

        public void CreateLogPackage(string filePath)
        {
            var tempFolder = Path.GetTempPath() + "\\LogPackage";

            // Remove temp directory if it exists already
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            // Create empty folder
            Directory.CreateDirectory(tempFolder);

            // Copy all necessary files in the temp folder: Logs and configs
            var tempLogFolder = tempFolder + "\\logs";
            var logFolder = $"{AppDomain.CurrentDomain.BaseDirectory}\\{LogFolderName}";
            Directory.CreateDirectory(tempLogFolder);

            // Copy logs
            if (Directory.Exists(logFolder))
            {
                foreach (var s in Directory.GetFiles(logFolder))
                {
                    var file = new FileInfo(s);
                    file.CopyTo($"{tempLogFolder}\\{file.Name}");
                }
            }

            //CopyF
            File.Copy(FilePaths.ServerConnectionsConfigPath, $"{tempFolder}\\ServerConnections.xml");

            // Create zip file from temp folder
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ZipFile.CreateFromDirectory(tempFolder, filePath);

            // Delete temp folder
            Directory.Delete(tempFolder, true);
        }

        public string LoadComputerName()
        {
            return Environment.MachineName;
        }

        public string LoadQstVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
