using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace s0s_L_ckr
{
    public sealed class Comn
    {
        private Comn()
        {
        }

        public static readonly Random rand = new Random();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray(ref byte[] array)
        {
            //CLEAR ARRAY FROM MEM
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (byte)rand.Next(0, 255);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void ClearString(ref string strg)
        {
            //CLEAR STRING FROM MEM
            if (strg == null)
                return;
            int strgLen = strg.Length;
            fixed (char* ptr = strg)
            {
                for (int i = 0; i < strgLen; i++)
                {
                    ptr[i] = (char)rand.Next(0, 255);
                }
             }
        }


        public static void OpenSecStr(ref String dest, ref SecureString srce)
        {
            //OPEN SECURE STRING METHOD
            IntPtr unmanStr = IntPtr.Zero;

            try
            {
                unmanStr = Marshal.SecureStringToGlobalAllocUnicode(srce);
                dest = Marshal.PtrToStringUni(unmanStr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanStr);
            }
        }

        public static Boolean CheckSig(FileInfo file)
        {
            //CHECK FILE SIG

            Boolean result = true;

            using (FileStream fileStm = File.OpenRead(file.FullName))
            {
                byte[] bData = new byte[ConfigMgr.FILE_SIGN_SIZE];
                fileStm.Read(bData, 0, bData.Length);

                for (int i = 0; i < ConfigMgr.FILE_SIGN_SIZE; i++)
                {
                    if (!(bData[i] == ConfigMgr.FILE_SIGN[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;

        }

        public static bool DireInFil(string fullNam)
        {
        }

        public static bool FileInFil(string fileExt)
        {

        }
    }
}
