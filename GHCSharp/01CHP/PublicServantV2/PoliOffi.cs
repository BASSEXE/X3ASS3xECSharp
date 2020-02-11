using System;

namespace PublicServantV2
{
    public class PoliOffi : PublicServantV2, IPUBLSERV
    {
        private bool _hasEmer = false;

        public PoliOffi(string name, int age, bool hasEmer = false) {
            this.Name = name;
            this.Age = age;
            this._hasEmer = hasEmer;

            if (this.HasEmer)
            {
                this.DTPOI += delegate
                {
                    Console.WriteLine("Driving the police car with siren");
                    GIPC();
                    TOS();
                    FDs();
                };
            }else{
                this.DTPOI += delegate
                {
                    Console.WriteLine("Driving the police car");
                    GIPC();
                    FDs();
                };
            } 
        }
        //Implementation of IPublic Servant Interface
        public string Name { get; set; }
        public int Age { get; set; }

        public bool HasEmer {
            get { return _hasEmer; }
            set { _hasEmer = value; }
        }

        private void GIPC() { }
        private void TOS() { }
        private void FDs() { }
    }
}