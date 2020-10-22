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
            cDrvs.AddRange(GetSpcl_Folders());
            return cDrvs;
        }

        private List<string> GetSpcl_Folders() => new List<string>()
            {
                Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
            };
    }
}
