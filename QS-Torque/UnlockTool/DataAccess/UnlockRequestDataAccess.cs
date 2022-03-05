using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnlockTool.Entities;
using UnlockTool.UseCases;

namespace UnlockTool.DataAccess
{
    public class UnlockRequestDataAccess : IUnlockRequestDataAccess
    {
        private const int hashstringsize = 128;
        private const string salt = "UNLOCKHASH";

        private const string KEYVALUESEPARATOR = "=";
        private const string QSTVERSIONKEY = "qstversion";
        private const string UNLOCKREQUESTVERSIONKEY = "unlockrequestversion";
        private const string NAMEKEY = "name";
        private const string PHONENUMBERKEY = "phonenumber";
        private const string COMPANYKEY = "company";
        private const string ADDRESSKEY = "address";
        private const string EMAILKEY = "email";
        private const string WINDOWSKEY = "windows";
        private const string PCNAMEKEY = "pcname";
        private const string FQDNKEY = "fqdn";
        private const string LOGEDINUSERNAMEKEY = "logedinusername";
        private const string CURRENTUTCDATETIMEKEY = "currentutcdatetime";
        private const string CURRENTLOCALDATETIMEKEY = "currentlocaldatetime";
        

        private Dictionary<string,string> GetKeyValuePairsFromBody(string body)
        {
            StringReader reader = new StringReader(body);
            var line = reader.ReadLine();
            Dictionary<string,string> keyvalue = new Dictionary<string, string>();
            while(line != null)
            {
                int index = line.IndexOf("=");
                if (index > -1)
                {
                    string key = line.Substring(0, line.IndexOf("="));
                    string value = line.Length > (index + 1)
                        ? line.Substring(index + 1, line.Length - (index + 1))
                        : "";
                    keyvalue.Add(key,value);
                }

                line = reader.ReadLine();
            }

            return keyvalue;
        }

        private void MapDictionaryToEntity(Dictionary<string, string> keyvaluepairs, ref UnlockRequest ur)
        {
            string value;
            if (keyvaluepairs.TryGetValue(QSTVERSIONKEY, out value)){ ur.QSTVersion = value; }

            if (keyvaluepairs.TryGetValue(UNLOCKREQUESTVERSIONKEY, out value))
            {
                ulong version;
                if (ulong.TryParse(value, out version))
                {
                    ur.UnlockRequestVersion = version;
                }
                
            }
            if (keyvaluepairs.TryGetValue(NAMEKEY, out value)) { ur.Name = value; }
            if (keyvaluepairs.TryGetValue(PHONENUMBERKEY, out value)) { ur.Phonenumber = value; }
            if (keyvaluepairs.TryGetValue(COMPANYKEY, out value)) { ur.Company = value; }
            if (keyvaluepairs.TryGetValue(ADDRESSKEY, out value)) { ur.Address = value; }
            if (keyvaluepairs.TryGetValue(EMAILKEY, out value)) { ur.Email = value; }
            if (keyvaluepairs.TryGetValue(WINDOWSKEY, out value)) { ur.Windows = value; }
            if (keyvaluepairs.TryGetValue(PCNAMEKEY, out value)) { ur.PCName = value; }
            if (keyvaluepairs.TryGetValue(FQDNKEY, out value)) { ur.FQDN = value; }
            if (keyvaluepairs.TryGetValue(LOGEDINUSERNAMEKEY, out value)) { ur.LogedinUserName = value; }
            if (keyvaluepairs.TryGetValue(CURRENTUTCDATETIMEKEY, out value)) { ur.CurrentUtcDateTime = value; }
            if (keyvaluepairs.TryGetValue(CURRENTLOCALDATETIMEKEY, out value)) { ur.CurrentLocalDateTime = value; }
        }

        public UnlockRequest GetUnlockRequestFromStream(Stream stream)
        {
            if (stream.Length > hashstringsize)
            {
                var erg = new UnlockRequest();
                int bodylength = (int) stream.Length - hashstringsize;
                byte[] buffer = new byte[bodylength];
                stream.Read(buffer, 0, bodylength);
                var body = Encoding.UTF8.GetString(buffer);
                SHA512 shaM = new SHA512Managed();
                byte[] result;
                result = shaM.ComputeHash(buffer);
                var calcedFileHash = BitConverter.ToString(result);
                calcedFileHash = calcedFileHash.Replace("-", "");
                calcedFileHash =
                    BitConverter.ToString(shaM.ComputeHash(Encoding.ASCII.GetBytes(salt + calcedFileHash + salt)));
                calcedFileHash = calcedFileHash.Replace("-", "");
                byte[] bufferForFileHash = new byte[hashstringsize];
                stream.Read(bufferForFileHash, 0, hashstringsize);
                var hashFromFile = Encoding.ASCII.GetString(bufferForFileHash);
                erg.HashOk = calcedFileHash == hashFromFile;
                MapDictionaryToEntity(GetKeyValuePairsFromBody(body), ref erg);
                return erg;
            }
            return new UnlockRequest();
        }
    }
}
