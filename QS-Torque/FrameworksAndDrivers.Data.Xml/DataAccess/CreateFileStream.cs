using FrameworksAndDrivers.Data.Xml.Interface;
using System.IO;

namespace FrameworksAndDrivers.Data.Xml.DataAccess
{
    public class CreateFileStream : ICreateStream
    {
        public Stream InputStream(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            return File.Create(filePath);
        }

        public Stream OutputStream(string filePath)
        {
            return File.Exists(filePath) ? File.Open(filePath, FileMode.Open) : Stream.Null;
        }
    }
}
