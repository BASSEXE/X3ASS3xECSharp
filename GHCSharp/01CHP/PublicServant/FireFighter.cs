using System;

namespace _publicservant
{
  public class FireFighter : _PublServ, IPublicServant
  {
    public FireFighter(string name, int age) {
      this.Name = name;
      this.Age  = age;
    }

    //Interface Implementation.
    public string Name {get;set;}
    public int    Age  {get;set;}

    public override void DTPoI() {
      GIFt();
      TOS();
      FDs();
    }

    private void GIFt() {}
    private void TOS() {}
    private void FDs() {}
  }
}
