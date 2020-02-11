using System;
namespace PublicServantV2
{
    public abstract class PublicServantV2
    {
        public int PENSA { get; set; }
        public DTPOIDelegate DTPOI { get; set; }
        public delegate void DTPOIDelegate();
    }
}
