using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using FrameworksAndDrivers.Data.Xml.Interface;

namespace ServerConnections.DataAccess
{
    public class CreateFileStream : ICreateStream
    {
        public Stream InputStream(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var fileSecurity = new FileSecurity();
            fileSecurity.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.FullControl, AccessControlType.Allow));
            var file = File.Create(filePath, 4096, FileOptions.None);
            var fInfo = new FileInfo(filePath);
            fInfo.SetAccessControl(fileSecurity);
            return file;
        }

        public Stream OutputStream(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.Open(filePath, FileMode.Open);
            }
            else
            {
                return Stream.Null;
            }
            
        }
    }
}
