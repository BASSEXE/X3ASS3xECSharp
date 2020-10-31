namespace FR46_LCKR
    //Encryption provider
{
    internal interface IFil_Crypt
    {
        byte[] CreateEncryptionKey();
        byte[] GetEncryptionKey();
        byte[] EncryptBytes(byte[] fileBytes, byte[] encryptionKey);
    }
}
