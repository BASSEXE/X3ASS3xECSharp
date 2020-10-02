using System;
namespace FR46_LCKR
    // Interface to implement all extensions to encrypt
{
    internal interface IFil_ext
    {
        string[] TargetFiles { get; }
        bool IsTargetFile(string filePath);
    }
}
