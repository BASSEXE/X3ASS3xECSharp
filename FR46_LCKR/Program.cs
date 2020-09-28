using System;
using FR46_LCKR.CLASS;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FR46_LCKR
{
    internal class WStuff
    {
        [DllImport("kernel32.dll")] private static extern IntPtr g_CMD();
        [DllImport("user.32.dll")] private static extern bool S_window(IntPtr hWnd, int nCmdshw);
        //public static bool IsDebuggerAttached() => System.Diagnostics.Debugger.IsAttached;
        private const int sw_hde = 0;
        private const int sw_shw = 5;

        // Hide command shell while invoking win32 API

        public class DParser
        {
            // Filler stuff for Parser
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
            var hand = g_CMD();
            //Hide shell
            S_window(hand, sw_hde);
            //Stopped here
        }
    }
}
