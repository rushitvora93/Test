using Core.UseCases;
using FrameworksAndDrivers.Data.Xml.Interface;
using State;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using IServerConnectionStorage = ServerConnections.UseCases.IServerConnectionStorage;

namespace ServerConnections.DataAccess
{

    /// <summary>
    /// Manages the Xml File Access for the ServerConnection UseCase
    /// </summary>
    public class ServerConnectionStorageXml : IServerConnectionStorage
    {
        private readonly ICreateStream _createStream;
        private readonly string _filePath;

        public ServerConnectionStorageXml(ICreateStream createStream, string filePath)
        {
            this._createStream = createStream;
            this._filePath = filePath;
        }

        /// <summary>
        /// Gets the ServerConnectionList of an encrypted xml file
        /// </summary>
        /// <returns>Encrypted ServerConnectionList</returns>
        public List<ServerConnection> GetServerConnections()
        {
            //Get Stream from Interface
            using (var stream = _createStream.OutputStream(_filePath))
            {
                if (stream == Stream.Null)
                {
                    return new List<ServerConnection>();
                }
                //Create Managed Aes Encryption 
                using (var aesManaged = new AesManaged())
                    //Create Decryptor with Keys from EncryptionKeys Class
                using (var cryptoTransformDecrypt = aesManaged.CreateDecryptor(EncryptionKeys.CryptKey32Bytes, EncryptionKeys.CryptIv16Bytes))
                    //Open cryptoStream
                using (var cs = new CryptoStream(stream, cryptoTransformDecrypt, CryptoStreamMode.Read))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<ServerConnection>));
                    //Deserialize ServerConnectionList
                    var connections = (List<ServerConnection>)xmlSerializer.Deserialize(cs);
                    return connections;
                }
            }
            
        }

        /// <summary>
        /// Encryptes and saves a ServerConnectionList
        /// </summary>
        /// <param name="connections"></param>
        public void SaveServerConnections(List<ServerConnection> connections)
        {
            //Get Stream from Interface
            using (var stream = _createStream.InputStream(_filePath))
            //Create Managed Aes Encryption 
            using (var aesManaged = new AesManaged())
            //Create Encryptor with Keys from EncryptionKeys Class
            using (var cryptoTransformEncrypt = aesManaged.CreateEncryptor(EncryptionKeys.CryptKey32Bytes, EncryptionKeys.CryptIv16Bytes))
            //Open cryptoStream
            using (var cs = new CryptoStream(stream, cryptoTransformEncrypt, CryptoStreamMode.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ServerConnection>));
                xmlSerializer.Serialize(cs, connections);
            }
        }
    }
}
