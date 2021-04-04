using System;
using System.IO;
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

        public static void Encrypt(Stream stream, ref byte[] srceDat)
        {
            //ICryptoTransform - Defines the basic operations of cryptographic transformations.
            //Cryptographic interface
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
