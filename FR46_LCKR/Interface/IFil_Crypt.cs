using System;
namespace FR46_LCKR
    //Encryption provider
{
    internal interface IFil_Crypt
    {
        byte[] Create_ECrypt_Key();
        byte[] Get_ECrypt_Key();
        byte[] ECrypt_Bytes(byte[] fileBytes, byte[] encryptionKey);
    }
}
