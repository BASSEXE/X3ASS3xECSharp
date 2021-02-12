using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace s0s_l_ck3r
{
    public class CompIdStrat
    {
        private const string HARDWARE_INFO =

            //BIOS
            "Win32_BIOS;Manufacturer@Win32_BIOS;SMBIOSBIOSVersion@Win32_BIOS;IdentificationCode@Win32_BIOS;SerialNumber@Win32_BIOS;ReleaseDate@Win32_BIOS;Version"
            + "@" +
            //CPU
            "Win32_Processor;ProcessorId@Win32_Processor;Manufacturer"
            + "@" +
            //HD
            "Win32_DiskDrive;Model@Win32_DiskDrive;Manufacturer@Win32_DiskDrive;Signature@Win32_DiskDrive;TotalHeads"
            +"@"+
            //MB
            "Win32_BaseBoard;Model@Win32_BaseBoard;Manufacturer@Win32_BaseBoard;Name@Win32_BaseBoard;SerialNumber";

        public static StickyFingerPrinter(ref string id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] stats = HARDWARE_INFO.Split('@');
            foreach (string mainStats in stats)
            {
                string[] array_MainStats = mainStats.Split(';');
                string iD = GetID(array_MainStats[0], array_MainStats[1]);
            }
        }

        private static string GetID(string wmiClass, string wmiProp)
        {
            string result = "";
            System.Management.ManagementClass
        }
    }
}
