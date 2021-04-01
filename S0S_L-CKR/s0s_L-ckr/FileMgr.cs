using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace s0s_L_ckr
{
    public sealed class FileMgr
    {
        private FileMgr(){}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReadFil(FileInfo file, ref Byte[] fileDat)
        {
            using (FileStream fileStream = File.OpenRead(file.FullName))
            {
                fileDat = new byte[fileStream.Length];
                fileStream.Read(fileDat, 0, fileDat.Length);
            }
        }
    }
}
