using Core.UseCases;
using FrameworksAndDrivers.Data.Xml.Interface;
using NUnit.Framework;
using ServerConnections.DataAccess;
using State;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace ServerConnections.Test.DataAccess
{
    public class ServerConnectionStorageXmlTest
    {

        class CreateMemoryStreamMock : ICreateStream
        {
            public OwnMemmoryStreamMock MemmoryStreamMock;
            public CreateMemoryStreamMock()
            {
                MemmoryStreamMock = new OwnMemmoryStreamMock();
            }

            public Stream InputStream(string filePath)
            {
                MemmoryStreamMock = new OwnMemmoryStreamMock();
                return MemmoryStreamMock;
            }

            public Stream OutputStream(string filePath)
            {
                return MemmoryStreamMock;
            }
        }

        private readonly string _filePath = "./ServerTest.xml";
        private List<ServerConnection> _serverConList = new List<ServerConnection>();
        private ServerConnectionStorageXml _serverConnectionStorageXml;
        private CreateMemoryStreamMock _createMemoryStreamMock;

        class OwnMemmoryStreamMock : MemoryStream
        {
            protected override void Dispose(bool disposing)
            {
            }

            public void OwnDispose()
            {
                base.Dispose(true);
            }
        }

        [SetUp]
        public void SetUp()
        {
            _createMemoryStreamMock = new CreateMemoryStreamMock();
            _serverConnectionStorageXml = new ServerConnectionStorageXml(_createMemoryStreamMock, _filePath);
            _serverConList = new List<ServerConnection>
            {
                new Core.UseCases.ServerConnection
                {
                    HostName = "HostName",
                    Port = 55555,
                    PrincipalName = "PrincipleName",
                    ServerName = "ServerName"
                },
                new Core.UseCases.ServerConnection
                {
                    HostName = "HostName2",
                    Port = 44444,
                    PrincipalName = "PrincipleName2",
                    ServerName = "ServerName2"
                }
            };
        }

        [Test]
        public void GetServerConnectionsTest()
        {
            WriteServerConnectionList(_serverConList, _createMemoryStreamMock.MemmoryStreamMock);
            Assert.AreEqual(_serverConList, _serverConnectionStorageXml.GetServerConnections());
        }

        [Test]
        public void SaveServerConnectionFileExists()
        {
            _serverConnectionStorageXml.SaveServerConnections(_serverConList);
            List<ServerConnection> readConnectionList =  ReadServerConnectionList(_createMemoryStreamMock.MemmoryStreamMock);
            Assert.AreEqual(_serverConList, readConnectionList);
        }

        [Test]
        public void WriteServerConnectionList()
        {
            _serverConnectionStorageXml.SaveServerConnections(_serverConList);
            var readServerConList = ReadServerConnectionList(_createMemoryStreamMock.MemmoryStreamMock);
            Assert.AreEqual(_serverConList, readServerConList);
        }

        [TearDown]
        public void FileAccessTearDown()
        {
            _createMemoryStreamMock.MemmoryStreamMock.OwnDispose();
        }

        private void WriteServerConnectionList(List<ServerConnection> connections, Stream stream)
        {
            using (var aesManaged = new AesManaged())
            using (var encryptor = aesManaged.CreateEncryptor(EncryptionKeys.CryptKey32Bytes, EncryptionKeys.CryptIv16Bytes))
            using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ServerConnection>));
                xmlSerializer.Serialize(cryptoStream, connections);
            }
            stream.Position = 0;
        }


        private List<ServerConnection> ReadServerConnectionList(Stream stream)
        {
            stream.Position = 0;
            using (var aesManaged = new AesManaged())
            using (var decryptor = aesManaged.CreateDecryptor(EncryptionKeys.CryptKey32Bytes, EncryptionKeys.CryptIv16Bytes))
            using (var cs = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ServerConnection>));
                var connections = (List<ServerConnection>)xmlSerializer.Deserialize(cs);
                return connections;
            }
        }
    }
}