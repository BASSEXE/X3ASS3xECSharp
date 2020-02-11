using System;
namespace PublicServantV2
{
    public class FireFigh : PublicServantV2, IPUBLSERV
    {
        public FireFigh(string name, int age){
            this.Name = name;
            this.Age = age;
            this.DTPOI += delegate
            {
                Console.WriteLine("Driving the firetruck");
                GIFt();
                TOS();
                FDs();
            };
        }

        //Ipublicservant interface implementation
        public string Name { get; set; }
        public int Age { get; set; }

        private void GIFt() { }
        private void TOS() { }
        private void FDs() { }
    }
}
