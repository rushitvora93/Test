using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using UnlockToolShared.Entities;
using UnlockTool.UseCases;
using UnlockToolShared.DataAccess;

namespace UnlockTool.DataAccess
{
    public class UnlockResponseWriteDataAccess : IUnlockResponseWriteDataAccess
    {
       private const string keybase64 = @"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";
       private byte[] SignAndEncrypt(byte[] input)
       {
            var rsa = new RSACryptoServiceProvider(ResponseSettings.rsakeysize);
            int bytesred = 0;
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(keybase64), out bytesred);
            byte[] signature = rsa.SignData(input, ResponseSettings.HashAlgorithmName, ResponseSettings.RsaSignaturePadding);
            var list = input.ToList();
            list.AddRange(signature.ToList());
            using (var aes = Aes.Create())
            {
                aes.KeySize = ResponseSettings.aeskeysize;
                aes.IV = Convert.FromBase64String(ResponseSettings.aesivbase64);
                aes.Key = Convert.FromBase64String(ResponseSettings.aeskeybase64);
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var stream = new MemoryStream())
                {
                    using (var cryptostream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptostream.Write(list.ToArray());
                        cryptostream.Flush();
                        cryptostream.FlushFinalBlock();
                        return stream.ToArray();
                    }
                }

            }
       }

        public void WriteUnlockResponse(UnlockResponse ur, Stream stream)
        {
            string jsonString = JsonSerializer.Serialize(ur);
            var shaM = new SHA512Managed();
            byte[] result;
            result = shaM.ComputeHash(Encoding.UTF8.GetBytes(ResponseSettings.salt +jsonString+ ResponseSettings.salt));
            var calcedFileHash = BitConverter.ToString(result);
            calcedFileHash = calcedFileHash.Replace("-", "");
            stream.Write(SignAndEncrypt(Encoding.UTF8.GetBytes(jsonString + calcedFileHash)));
        }
    }
}
