using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ASE2._0
{
    public class CustomQuery
    {
        public CustomQuery()
        {
            Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            string submUrl = Console.ReadLine();
            string urlNBI = IsHTTP(submUrl);

            string[] parameters = urlNBI.Remove(0, urlNBI.IndexOf("?") + 1).Split('&');
            Console.Write("Paremeters to be injected are: ");
            foreach (string p in parameters)
            {
                Console.Write($"{p}, ");
            }

            string[] sQLIResult = NullByteInjection(parameters, urlNBI);


            static string[] NullByteInjection(string[] nbiParms, string url)
            {
                string nBIParameters = "Nullbyte";
                int i = nbiParms.Length;

                for (int j = 0; j < i; j++)
                {
                    foreach (string s in nbiParms)
                    {
                        //string nbi = url.Replace(s, s + "%00");
                        //Console.WriteLine($"Injecting {nbi} for SQLI Vulnerability");

                        HttpWebRequest nbiWebRequest = (HttpWebRequest)WebRequest.Create(s);
                        nbiWebRequest.Method = "GET";

                        string sqliResp = string.Empty;
                        try
                        {
                            using (StreamReader rdr = new
                                StreamReader(nbiWebRequest.GetResponse().GetResponseStream()))
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
                        System.Console.WriteLine("Null Byte Injection Sent");
                        System.Console.WriteLine(sqliResp);

                        return new[] { nBIParameters };

                    }

                }
                return new[] { nBIParameters };

            }


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
