using System;
//Interface to implement sending of Encryption Key
namespace FR46_LCKR.Interface
{
     internal interface IComms
    {
        void S_Data(string ECryption_Key);
    }
}
