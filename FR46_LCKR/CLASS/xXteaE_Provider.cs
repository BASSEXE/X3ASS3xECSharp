using System;
using System.Security.Cryptography;
using System.Text;
namespace FR46_LCKR.CLASS
{
    internal class xXteaE_Provider : IFil_Crypt
    {
        private readonly UTF8Encoding utf8 = new UTF8Encoding();
        private byte[] EncryptionKey { get; set; }

        //Create AES encryption key

        public byte[] Create_ECrypt_Key()
        {
            var kP = new AesCryptoServiceProvider();
            kP.KeySize = 128;
            kP.GenerateKey();

            var key = kP.Key;

            kP.Dispose();
            EncryptionKey = key;
            return key;
        }

        //return encryption key

        public byte[] Get_ECrypt_Key()
        {
            return EncryptionKey;
        }

        public byte[] ECrypt_Bytes(byte[] fileBytes, byte[] encryptionKey)
        {
            throw new NotImplementedException();
        }

        private const UInt32 delta = 0x9E3778B9;

        private UInt32 MX(UInt32 sum, UInt32 y, UInt32 z, Int32 p, UInt32 e, UInt32[] k)
        {
            return (z >> 5 ^ y << 2) + (y >> 3 ^ z << 4) ^ (sum ^ y) + (k[p & 3 ^ e] ^ z);
        }

        private Byte[] Encrypt(Byte[] data, Byte[] k)
        {

        }
    }
}
