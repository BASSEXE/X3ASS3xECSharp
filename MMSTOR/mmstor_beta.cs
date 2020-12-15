using System;
using System.Net;
using System.IO;

namespace mmstor
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //var URL = args[0];
            string URL = "http://172.16.1.129/cgi-bin/badstore.cgi?searchquery=test&action=search&x=0&y=0";
            //<------Forgot "" marks for original argument and was dumbfounded why no errors were popping up. Its okay we are all newbs in life.
            string[] parameters = URL.Remove(0, URL.IndexOf("?") + 1).Split('&');
            Console.WriteLine("Paremeters to be tested are:");
            foreach (string parm in parameters)
                Console.WriteLine(parm);
            
            foreach (string parm in parameters)
            {
                string XSSI = URL.Replace(parm, parm + "fd<xss>sa");
                Console.WriteLine("Testing " + XSSI + " for XSS Vunerability");

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(XSSI);
                webRequest.Method = "GET";

                string XSSIresp = string.Empty;
                using (StreamReader reader = new
                    StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    XSSIresp = reader.ReadToEnd();
                }

                if (XSSIresp.Contains("<xss>"))
                {
                    Console.WriteLine("**\n Found Possible XSS point in parameter: "+ parm+"\n**");
                }

            }
           
            foreach (string parm in parameters)
            {
                string SQLI = URL.Replace(parm, parm + "'sa");
                Console.WriteLine("Testing "+ SQLI +" for SQLI Vulnerability ");

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(SQLI);
                webRequest.Method = "GET";

                string SQLIresp = string.Empty;
                using (StreamReader reader = new
                    StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    SQLIresp = reader.ReadToEnd();
                    if (SQLIresp.Contains("SQL syntax"))
                    {
                        Console.WriteLine("**\nFOUND POSSIBLE SQL INJECTION in "+parm+"\n**");
                    }
                    else
                    {
                        Console.WriteLine( parm + " not vulnerable");
                    }
                }
            }
        }
    }
}
