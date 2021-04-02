using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace s0s_L_ckr
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
            + "@" +
            //MB
            "Win32_BaseBoard;Model@Win32_BaseBoard;Manufacturer@Win32_BaseBoard;Name@Win32_BaseBoard;SerialNumber";

        public static void StickyFingerPrinter(ref string iD)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] stats = HARDWARE_INFO.Split('@');
            foreach (string mainStats in stats)
            {
                string[] array_MainStats = mainStats.Split(';');
                string identifier = GetID(array_MainStats[0], array_MainStats[1]);
                stringBuilder.AppendLine(iD);
                Comn.ClearString(ref iD);
            }

            iD = GetHash(stringBuilder.ToString());
        }

        private static string GetHash(string ghash)
        {
            MD5 mD5hash = new MD5CryptoServiceProvider();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bt = encoding.GetBytes(ghash);
            return GetHexString(mD5hash.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte c = bt[i];
                int n, n1, n2;
                n = (int)c;
                n1 = (n >> 4) & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        private static string GetID(string wmiClass, string wmiProp)
        {
            string result = "";
            System.Management.ManagementClass managementClass = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (System.Management.ManagementObject managementObject in managementObjectCollection)
            {
                if (result == "")
                {
                    try
                    {
                        result = managementObject[wmiProp].ToString();
                        break;
                    }
                    catch
                    {
                        result = "foo";
                    }
                }
            }
            return result;

        }
    }
}


