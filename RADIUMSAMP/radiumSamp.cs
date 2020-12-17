using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace RADIUMSAMP
{
    class MainClass
    {
        private const char Separator = ' ';
        private const string Value = "You have an error in your SQL syntax";

        public static void Main(string[] args)
        {
            string[] postRequestLns = File.ReadAllLines(args[0]);
            string[] parameters = postRequestLns[postRequestLns.Length - 1].Split('&');
            string host = string.Empty;
            StringBuilder postRequestBuilder = new StringBuilder();

            foreach (string ln in postRequestLns)
            {
                if (ln.StartsWith("Host:"))
                    host = ln.Split(Separator)[1].Replace("\r", string.Empty);
                postRequestBuilder.Append(ln + "\n");
            }
            string postRequest = postRequestBuilder.ToString() + "\r\n";
            Console.WriteLine(postRequest);
            string value = $"Host is {host} and parameters are {parameters[0]} , {parameters[1]}, {parameters[2]}";
            Console.WriteLine(value);

            IPEndPoint rHost = new IPEndPoint(IPAddress.Parse(host), 80);
            foreach (string parm in parameters)
            {
                Socket sockt = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                sockt.Connect(rHost);

                string parmValue = parm.Split('=')[1];
                string SqliPostRequest = postRequest.Replace("=" + parmValue,
                                                                "=" + parmValue + "'");
                byte[] postRequestBytes = Encoding.ASCII.GetBytes(SqliPostRequest);
                sockt.Send(postRequestBytes);

                string postResponse = string.Empty;
                byte[] responseBuffer = new byte[sockt.ReceiveBufferSize];

                sockt.Receive(responseBuffer);
                postResponse = Encoding.ASCII.GetString(responseBuffer);

                if (postResponse.Contains(Value))
                {
                    Console.WriteLine($"**\nFOUND POSSIBLE SQL INJECTION in {parm}\n**");
                }
                else
                    Console.WriteLine($"{parm} does not seem vulnerable to SQLI");

                sockt.Close();

            }
        }
    }
}
