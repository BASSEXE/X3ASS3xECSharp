using System;
using System.Linq;

namespace FR46_LCKR
{
    internal class Fil_ext : IFil_ext
    {
        public string[] TargetFiles { get; } =
            {
            ".test"
        };

        public bool IsTargetFile(string fPath)
        {
            var fileExtension = System.IO.Path.GetExtension(fPath).ToLower();
            return TargetFiles.Contains(fileExtension);
        }
    }
}
