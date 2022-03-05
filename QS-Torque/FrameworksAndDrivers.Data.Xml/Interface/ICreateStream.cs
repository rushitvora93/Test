using System.IO;

namespace FrameworksAndDrivers.Data.Xml.Interface
{
    public interface ICreateStream
    {
        Stream InputStream(string filePath);

        Stream OutputStream(string filePath);
    }
}
