using System.Security.Cryptography;

namespace UnlockToolShared.DataAccess
{
    public static class ResponseSettings
    {
        public const string aesivbase64 = @"MB4WjIqUDK6nn/kFcB1wAQ==";
        public const string aeskeybase64 = @"qY0BnBT39+y21zgfJNi6h1IeUEgtbmIrYD+1FDPPXEs=";
        public const int hashstringsize = 128;
        public const string salt = "3424k92034asdf=ASD!";
        public const int rsakeysize = 4096;
        public const int aeskeysize = 256;
        public static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;
        public static readonly RSASignaturePadding RsaSignaturePadding = RSASignaturePadding.Pkcs1;

    }
}
