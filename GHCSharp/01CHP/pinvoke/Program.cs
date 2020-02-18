using System;
using System.Runtime.InteropServices;

namespace Pinvoke
{
    class Program
    {
      [DllImport("user32", CharSet=CharSet.Auto)]
      static extern int MessageBox(IntPtr hWnd,
              String text, String Caption, int options);
      [DllImport("libc")]
      static extern void printf(string message);


        static void Main(string[] args)
        {
          OperatingSystem os = Environment.OSVersion;

          if(os.Platform == PlatformID.Win32Windows ||  os.Platform == PlatformID.Win32NT)
          {
            MessageBox (IntPtr.Zero,  "Hello World!", "Hello World!", 0);
          }
          else
          {
            printf ("Hello World!");
          }
        }
    }
}
