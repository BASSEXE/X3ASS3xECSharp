using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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
            /*string[] stats = HARDWARE_INFO.Split('@');
            foreach (string mainStats in stats)
            {
                string[] array_MainStats = mainStats.Split(';');
                string identifier = GetID(array_MainStats[0], array_MainStats[1]);
                stringBuilder.AppendLine(identifier);
                Comn.ClearString(ref identifier);
            }*/

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
            StringBuilder hex = new StringBuilder(bt.Length * 2);
            foreach (byte b in bt)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            s = hex.ToString();
            return s;
        }

        /*
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

        }*/

        }
    }



