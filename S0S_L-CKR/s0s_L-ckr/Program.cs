using System.Diagnostics;
using System.Threading;



namespace s0s_L_ckr
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            string ops = (args.Length > 0 && args[0] == "--decrypt" ? "D" : "E");
            
            #if DEBUG
                        //Debug Generation
                        string fingerprint = null;
                        CompIdStrat.StickyFingerPrinter(ref fp);

                        Trace.WriteLine("[+] System FingerPrint: " + fp);
            #endif
           



        }
    }
}
