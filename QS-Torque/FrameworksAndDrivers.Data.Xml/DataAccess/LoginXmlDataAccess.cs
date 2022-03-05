using FrameworksAndDrivers.Data.Xml.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Core.UseCases;
using State;
using Microsoft.Win32;

namespace FrameworksAndDrivers.Data.Xml.DataAccess
{
    public class LoginXmlDataAccess : IServerConnectionStorage
    {
        private readonly string _filePath;
        private readonly ICreateStream _stream;

        private static readonly string SuggestionUserNamesFileName = "LastUsers";
        private static readonly string LastServerConnectionFileName = "LastServerConnection";

        [Serializable]
        public class ServerConnectionListSerializaionWrapper
        {
            public ServerConnection ActiveConnection { get; set; }
            public List<ServerConnection> ServerConnectionList { get; set; }
        }

        public LoginXmlDataAccess(ICreateStream stream, string filePath)
        {
            _stream = stream;
            this._filePath = filePath;
        }

        public List<ServerConnection> LoadServerConnections()
        {
            //Get Stream from Interface
            using (var stream = _stream.OutputStream(_filePath))
            {
                if (stream == Stream.Null)
                {
                    return new List<ServerConnection>();
                }
                List<ServerConnection> connections;
                //Create Managed Aes Encryption 
                using (var aesManaged = new AesManaged())
                //Create Decryptor with Keys from EncryptionKeys Class
                using (var cryptoTransformDecrypt = aesManaged.CreateDecryptor(EncryptionKeys.CryptKey32Bytes, EncryptionKeys.CryptIv16Bytes))
                //Open cryptoStream
                using (var cs = new CryptoStream(stream, cryptoTransformDecrypt, CryptoStreamMode.Read))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<ServerConnection>));
                    //Deserialize ServerConnectionList
                    connections = (List<ServerConnection>)xmlSerializer.Deserialize(cs);
                }
                return connections;
            }
        }

        public List<string> LoadSuggestionUserNames()
        {
            return ReadUserNames();
        }

        public void AddUserNameToSuggestions(string userName)
        {
            var fullPath = Path.Combine(FilePaths.QstTempPath, SuggestionUserNamesFileName);
            if (!File.Exists(fullPath))
            {
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(FilePaths.QstTempPath);
                }

                using (File.Create(fullPath)) { }
            }

            var existingUserNames = ReadUserNames();
            if (existingUserNames.Contains(userName))
            {
                return;
            }
            if (existingUserNames.Count >= 5)
            {
                existingUserNames = existingUserNames.GetRange(0, 4);
            }
            existingUserNames.Insert(0,userName);

            using (var writer = new StreamWriter(fullPath))
            {
                foreach (var existingUserName in existingUserNames)
                {
                    writer.WriteLine(existingUserName);
                }
            }
        }

        
        public void AddLastServerConnection(ServerConnection serverConnection)
        {
            var fullPath = Path.Combine(FilePaths.QstTempPath, LastServerConnectionFileName);
            if (!File.Exists(fullPath))
            {
                if (!Directory.Exists(FilePaths.QstTempPath))
                {
                    Directory.CreateDirectory(FilePaths.QstTempPath);
                }
                using (File.Create(fullPath)){}
            }

            using(var writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(serverConnection.ServerName);
            }
        }

        /// <summary>
        /// Read the last userNames from temp Folder
        /// </summary>
        /// <returns>list of usernames</returns>
        private static List<string> ReadUserNames()
        {
            var fullPath = Path.Combine(FilePaths.QstTempPath, SuggestionUserNamesFileName);
            var lastUserNames = new List<string>();
            if (!File.Exists(fullPath))
            {
                return lastUserNames;
            }
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lastUserNames.Add(line);
                }
            }
            return lastUserNames;
        }

        public string LoadLastServerConnectionName()
        {
            var fullPath = Path.Combine(FilePaths.QstTempPath, LastServerConnectionFileName);
            if (!File.Exists(fullPath))
            {
                return null;
            }

            using (var reader = new StreamReader(fullPath))
            {
                return reader.ReadLine();
            }
        }
    }
}
