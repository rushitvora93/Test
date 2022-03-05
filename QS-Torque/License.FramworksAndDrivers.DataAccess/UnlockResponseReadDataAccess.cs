using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using UnlockToolShared.Entities;
using UnlockToolShared.UseCases;

namespace UnlockToolShared.DataAccess
{
    public class UnlockResponseReadDataAccess : IUnlockResponseReadDataAccess
    {
        private const string modulusbase64 = @"0Uw721BTSScV4R40pdcKvufRm2N/bEsnSKgsDU3pPmLIll+dl4wXAehZF/HcmC5fGLvyg7dmZe9e9HdY5Xvuy0iEJ49xGKYucGjKyQzBnWj1m/w4SBADTFq+fbT4oLpV10dLhM0b45SEJZMkS77y1Ran57nBs+rxr5OMYgsvjWOYcpQJKrhbyS+MZPPvguvI/PSPnqoKlSEydBqlXQXufkulddJfDJSG4G1HPa2ao7kItuoLRcrBCEf646/fpAfVSmbkQ0B5spf9jUYf84YpnW8wWeUnY7HT8qqIkAbiMq8kYVkuMapdsgd3GyVjvec3cimnysqgvrgzS4HHBI0XcF5tkamk0xLJf/Sth5uy3wpVVyM2jZoryRWo/yttC7wXdq9RgidUYKvVDdptUzX1HHcjNbF8eVZDHcOAz6gfNHSVe3kqgqDQ17s0oOw1ZBH0e0keFEfI/Ka9e6UeRkCNLLhDVfQDMvUMj1g4I3FNeLwVoHfbwfm+vGBVjxcf+xUGDY1CcziLorSnO6sJji3yaE99sNeTJbBGDZbJHx1t0axEt9vOMVmGajZuxiQMj6Rjomp6vmJ63S5mz7UKwKVhshx49iT/V+mZTT96sQt0P8dYC9Ggzg/QFUiuZB/yAN2CnRUIQVEa91PcLKOgY58k3NIY7IEQvWOUBrMYIMxoL80=";
        private const string exponentbase64 = @"AQAB";
        private byte[] DecryptAndVerifyByRsa(Stream stream)
        {
            stream.Position = 0;
            using (var aes = Aes.Create())
            {
                aes.KeySize = ResponseSettings.aeskeysize;
                aes.IV = Convert.FromBase64String(ResponseSettings.aesivbase64);
                aes.Key = Convert.FromBase64String(ResponseSettings.aeskeybase64);
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var lokalstream = new MemoryStream()) 
                {
                    using (var cryptostream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read ))
                    {
                        try
                        {
                            cryptostream.CopyTo(lokalstream);
                        }
                        catch (Exception e)
                        {
                            throw new InvalidOperationException("UnlockResponse: AES Decrypt Failed: " + e.Message);
                        }

                        lokalstream.Position = 0;
                        int signaturesize = ResponseSettings.rsakeysize / 8;
                        int bodysize = (int)lokalstream.Length - (signaturesize);
                        byte[] buffer = new byte[bodysize];
                        lokalstream.Read(buffer, 0, bodysize);
                        byte[] signature = new byte[signaturesize];
                        lokalstream.Read(signature, 0, signaturesize);
                        RSAParameters rsaParams = new RSAParameters();
                        rsaParams.Modulus = Convert.FromBase64String(modulusbase64);
                        rsaParams.Exponent = Convert.FromBase64String(exponentbase64);
                        var rsa = new RSACryptoServiceProvider(ResponseSettings.rsakeysize);
                        rsa.ImportParameters(rsaParams);
                        bool ok = rsa.VerifyData(buffer, signature, ResponseSettings.HashAlgorithmName, ResponseSettings.RsaSignaturePadding);
                        if (ok)
                        {
                            return buffer;
                        }

                        throw new InvalidOperationException("UnlockResponse: RSA Verify Failed");
                    }
                }

            }
        }

        public UnlockResponse ReadUnlockResponse(Stream stream)
        {
            using (var lokalstream = new MemoryStream(DecryptAndVerifyByRsa(stream)))
            {
                lokalstream.Position = 0;
                if (lokalstream.Length > ResponseSettings.hashstringsize)
                {
                    int bodylength = (int)lokalstream.Length - ResponseSettings.hashstringsize;
                    byte[] buffer = new byte[bodylength];
                    lokalstream.Read(buffer, 0, bodylength);
                    SHA512 shaM = new SHA512Managed();
                    byte[] result;
                    result = shaM.ComputeHash(Encoding.UTF8.GetBytes(
                        ResponseSettings.salt + Encoding.UTF8.GetString(buffer) + ResponseSettings.salt));
                    var calcedFileHash = BitConverter.ToString(result);
                    calcedFileHash = calcedFileHash.Replace("-", "");
                    byte[] bufferForFileHash = new byte[ResponseSettings.hashstringsize];
                    lokalstream.Read(bufferForFileHash, 0, ResponseSettings.hashstringsize);
                    var hashFromFile = Encoding.UTF8.GetString(bufferForFileHash);
                    bool HashOk = calcedFileHash == hashFromFile;
                    if (HashOk)
                    {
                        try
                        {
                            var unlockResponse =
                                (UnlockResponse) JsonSerializer.Deserialize(buffer, typeof(UnlockResponse));
                            return unlockResponse;
                        }
                        catch (Exception e)
                        {
                            throw new InvalidOperationException("UnlockResponse: Error by Parsing Json: " + e.Message);
                        }
                    }
                    throw new InvalidOperationException("UnlockResponse: SecondLevel Hash not Valid");
                }
            }

            throw new InvalidOperationException("UnlockResponse: FileSize to low");
        }
    }
}
