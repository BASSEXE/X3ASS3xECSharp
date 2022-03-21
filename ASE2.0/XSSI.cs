using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;


namespace ASE2._0
{
    public class XSSI
    {
        public XSSI()
        {
            Console.WriteLine("Supply the URL to test.../n URL should be in <url>.com?param=&param2...format");
            string submUrl = Console.ReadLine();
            string urlXSSI = IsHTTP(submUrl);

            string[] parameters = urlXSSI.Remove(0, urlXSSI.IndexOf("?") + 1).Split('&');
            Console.Write("Paremeters to be tested are: ");
            foreach (string p in parameters)
            {
                    Console.Write($"{p}, ");
            }

            string[] xxsiResult = XSSI(parameters, urlXSSI);
            string[] XSSI(string[] xssParams, string url)
            {



                string noVulnParms = "No parameters were found to be vulnerable to XSS attempts";
                foreach (string x in xssParams)
                {
                    bool isVuln = false;

                    int i = xssParams.Length;

                    string[] vulnParms = new string[i];


                    var xssi = url.Replace(x, x + "BASS<xss>EXE");
                    Console.WriteLine($"Testing {xssi} for XSS Vulnerability");
                    try
                    {

                        HttpWebRequest xssWebRequest = (HttpWebRequest)WebRequest.Create(xssi);
                        xssWebRequest.Method = "GET";

                        string xssiWebResponse = string.Empty;
                        try
                        {
                            using (StreamReader rdr = new
                              StreamReader(xssWebRequest.GetResponse().GetResponseStream()))
                            {
                                xssiWebResponse = rdr.ReadToEnd();
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

                        if (!xssiWebResponse.Contains("xss"))
                        {
                            Console.WriteLine(noVulnParms);
                            return new[] { noVulnParms };
                        }
                        else
                        {
                            Console.WriteLine($"**\n Found Possible XSS point in parameter: {x} \n**");
                            isVuln = true;
                        }

                        if (isVuln)
                        {
                            Console.WriteLine(xssParams);
                            return xssParams;
                        }

                    }
                    catch(UriFormatException ex)
                    {
                        if (ex.Message.Contains("Invalid URI"))
                        {
                            var response = ex.Message;
                            Console.WriteLine("URI format was malformed and couldn't be Parsed for HTTP Web Class:"+ ex.Message);
                        }

                    }


                }
                return new[] { noVulnParms };


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
