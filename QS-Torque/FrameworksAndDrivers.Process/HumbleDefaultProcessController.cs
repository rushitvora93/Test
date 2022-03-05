using System.IO;

namespace FrameworksAndDrivers.Process
{
    public class HumbleDefaultProcessController: IProcessController
    {
        public void StartProcess(string pathToBinary)
        {
            if (!File.Exists(pathToBinary))
            {
                throw new FileNotFoundException(pathToBinary);
            }
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = pathToBinary;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(pathToBinary);
            process.Start();
        }
    }
}