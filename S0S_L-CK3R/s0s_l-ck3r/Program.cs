using System.Diagnostics;
using System.Threading;



namespace s0s_l_ck3r
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            string ops = (args.Length > 0 && args[0] == "--decrypt" ? "D" : "E");
            /*
            #if DEBUG
                        //Debug Generation
                        string fingerprint = null;
                        ComputerIdStratgey.GenerateFP(ref fp);

                        Trace.WriteLine("[+] System FingerPrint: " + fp);
            #endif
            */



        }
    }
}
