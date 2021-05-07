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
        private readonly string HARDWARE_INFO = Environment.MachineName;

        public CompIdStrat(string hARDWARE_INFO) => HARDWARE_INFO = hARDWARE_INFO ?? throw new ArgumentNullException(nameof(hARDWARE_INFO));

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
            StringBuilder hex = new StringBuilder(bt.Length * 2);
            foreach (byte b in bt)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            string s = hex.ToString();
            return s;
        }

        private static string GetIdentifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
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



