using System.IO;
using FrameworksAndDrivers.Data.Xml.DataAccess;
using NUnit.Framework;

namespace FrameworksAndDrivers.Data.Xml.Test
{
    public class CreateFileStreamTest
    {
        
        [SetUp]
        public void Setup()
        {
            CreateFileStream = new CreateFileStream();
        }

        private CreateFileStream CreateFileStream;
        private static string TestfilePath = "./testfile.txt";
        private static string FileTestText = "Test";

        [Test]
        public void OutputStreamFileNotExists()
        {
            using (var output = CreateFileStream.OutputStream(TestfilePath))
            {
                Assert.AreEqual(output, Stream.Null);
            }
        }

        [Test]
        public void OutputStreamFileExists()
        {
            File.Create(TestfilePath).Dispose();
            using (var streamWriter = new StreamWriter(TestfilePath))
            {
                streamWriter.Write(FileTestText);
            }
            var readLine = "";
            using(var outputStream = CreateFileStream.OutputStream(TestfilePath))
            using (var streamReader = new StreamReader(outputStream))
            {
                readLine = streamReader.ReadLine();
            }
            Assert.AreEqual(readLine, FileTestText);
        }

        [Test]
        public void InputStreamFileNotExists()
        {
            using(var inputStream = CreateFileStream.InputStream(TestfilePath))
            {
                Assert.IsTrue(File.Exists(TestfilePath));
            }
        }

        [Test]
        public void InputStreamFileExists()
        {
            File.Create(TestfilePath).Dispose();
            using (var inputStream = CreateFileStream.InputStream(TestfilePath))
            using(var streamWriter = new StreamWriter(inputStream))
            {
                streamWriter.WriteLine(FileTestText);
            }

            var readLine = "";
            using (var streamReader = new StreamReader(TestfilePath))
            {
                readLine = streamReader.ReadLine();
            }
            Assert.AreEqual(readLine, FileTestText);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TestfilePath))
            {
                File.Delete(TestfilePath);
            }
        }
    }
}