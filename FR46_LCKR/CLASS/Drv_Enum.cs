using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FR46_LCKR.CLASS
{
    internal class Foldr_Browser : IDrv_Enum
    {
        public List<string> StartFolder()
        {
            var cDrvs = DriveInfo.GetDrives().Select(drive => drive.Name).ToList();
            cDrvs =.AddRange(GetSpcl_Folders());
            return cDrvs;
        }

        private List<string> GetSpcl_Folders()
        {

        }
    }
}
