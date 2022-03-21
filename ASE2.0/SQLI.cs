using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ASE2._0
{
    public class SQLI
    {
        public SQLI()
        {
            Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            string submUrl = Console.ReadLine();
            string urlSQLI = IsHTTP(submUrl);

            string[] parameters = urlSQLI.Remove(0, urlSQLI.IndexOf("?") + 1).Split('&');
            Console.Write("Paremeters to be tested are: ");
            foreach (string p in parameters)
            {
                Console.Write($"{p}, ");
            }

            string[] sQLIResult = SQLI(parameters, urlSQLI);




            /*
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             */
            static string[] SQLI(string[] sqliParams, string url)
            {
                bool isVuln = false;
                string noVulnParams = "No parameters were found to be vulnerable to SQLI attempts";

                int i = sqliParams.Length;
                string[] vulnParms = new string[i];
                string NoVulnParameters = "No Vuln parameters found..or end of code reached";

                for (int j = 0; j < i; j++)
                {
                    foreach (string s in sqliParams)
                    {
                        string sqli = url.Replace(s, s + "'SELECT *");
                        Console.WriteLine($"Testing {sqli} for SQLI Vulnerability");

                        HttpWebRequest sqliWebRequest = (HttpWebRequest)WebRequest.Create(sqli);
                        sqliWebRequest.Method = "GET";

                        string sqliResp = string.Empty;
                        try
                        {
                            using (StreamReader rdr = new
                                StreamReader(sqliWebRequest.GetResponse().GetResponseStream()))
                            {
                                sqliResp = rdr.ReadToEnd();
                            }
                        }
                        catch (WebException ex)
                        {
                            if (ex.Status == WebExceptionStatus.ProtocolError)
                            {
                                var response = ex.Response as HttpWebResponse;
                                if (response != null)
                                {
                                    Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                                }
                                else
                                {
                                    // no http status code available
                                }
                            }
                            else
                            {
                                // no http status code available
                            }
                        }
                        if (sqliResp.Contains("SQL syntax"))
                        {
                            Console.WriteLine("**\nFOUND POSSIBLE SQL INJECTION in " + s + "\n**");
                            isVuln = true;
                            vulnParms[j] = s;
                            return vulnParms;
                        }
                        else if (!sqliResp.Contains("SQL syntax"))
                        {
                            Console.WriteLine(noVulnParams);
                            return new[] { noVulnParams };
                        }


                    }
                }
                return new[] { NoVulnParameters };
            }


                static string IsHTTP(string url2Test)
                {
                    string httpURL = url2Test;
                    Regex httpRegex = new("(http:\\/\\/(www)?\\w.*)|(https:\\/\\/(www)?\\w.*)");
                    MatchCollection matchCollection = httpRegex.Matches(httpURL);
                    if (httpRegex.IsMatch(httpURL))
                    {
                        Console.WriteLine(httpURL);
                        return httpURL;
                    }
                    else
                    {
                        Console.WriteLine("{0} matches found in: URL Submission\n URL Submitted was not in HTTP/HTTPS format",
                             matchCollection.Count
                             );
                    if (matchCollection.Count == 0)
                    {
                        string httpURL2 = default;
                        Console.WriteLine("Missing protocol \n Adding https:// to url");
                        httpURL2 = "https://" + httpURL;
                        httpURL = httpURL2;
                        return httpURL;
                    }
                    string mC = matchCollection.ToString();
                    Console.WriteLine(mC);
                    return mC;
                }
            }

        }
    }
}
