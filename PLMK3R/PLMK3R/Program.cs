using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
/*
 * Requirments Map
 *  1. Accept User input
 *  2. is this a url
 *      2.a If protocol is not present 
 *          URL+="http://"+value
 *  3. Locate values to test
 *  4. Test for possible SQLI
 *  5. Build queries for use
 *  6. Gather information.
 *  7.Output Information
 *   
 */

namespace playMzker
{
    class MainClass
    {
        /*
         */
        public static void Main(string[] args)
        {

            Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            string url2Test = Console.ReadLine();
            string newURL2Test = IsHTTP(url2Test);
            Console.WriteLine($"Testing if {newURL2Test} is vulnerable to SQL Injection ");
            string[] urlParams = newURL2Test.Remove(0, newURL2Test.IndexOf("?") + 1).Split('&');

            foreach (string printParams in urlParams)
            {
                Console.WriteLine($"Parameters are {printParams}");
            }

            foreach (string newUrlParams in urlParams)
            {
                string SQLIURL = newURL2Test.Replace(newUrlParams, newUrlParams + "te'st");
                Console.WriteLine(SQLIURL);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(SQLIURL);
                httpWebRequest.Method = "GET";
                string SQLIresp = string.Empty;
                using (StreamReader reader = new
                    StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                {
                    SQLIresp = reader.ReadToEnd();
                }

                if (SQLIresp.Contains("error in your SQL syntax"))
                {
                    Console.WriteLine($"SQL Injection found in parameter: {newUrlParams}");
                }
                else
                    Console.WriteLine($"Parameter {newUrlParams} does not seem vulnerable");
            }
            //CreatSQLIRequest(urlParams, newURL2Test, SQLIURL);

            /*
            foreach (string getSQLIURL in SQLIURL)
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(getSQLIURL);
                httpWebRequest.Method = "GET";
                string SQLIresp = string.Empty;
                using (StreamReader reader = new
                    StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                {
                    SQLIresp = reader.ReadToEnd();
                }

                if (SQLIresp.Contains("error in your SQL syntax"))
                {
                    Console.WriteLine("SQL Injection found in parameter");
                }
            }
            */




        }
        /*
        public static string CreatSQLIRequest(string[] urlParams, string newURL2Test, string[] SQLIURL)
        {
            foreach (string newUrlParams in urlParams)
            {
                SQLIURL = newURL2Test.Replace(newUrlParams, newUrlParams + "te'st");
                Console.WriteLine(SQLIURL);
            }
            return SQLIURL;
        }
        */
        private static string IsHTTP(string url2Test)
        {
            string httpURL;
            if (!url2Test.Contains("http://"))
            {
                httpURL = "http://" + url2Test;
                url2Test = httpURL;
            }
            return url2Test;
        }
    }
}
