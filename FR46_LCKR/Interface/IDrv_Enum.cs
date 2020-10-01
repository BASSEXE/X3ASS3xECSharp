using System;
using System.Collections.Generic;
//Interface to Enumerate Drives mounted
namespace FR46_LCKR
{
    internal interface IDrv_Enum
    {
        List<string> StartFolder();
    }
}
