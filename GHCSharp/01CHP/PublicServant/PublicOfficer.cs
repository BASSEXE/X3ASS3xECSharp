using System;

namespace _publicservant
{
  public class PoliceOfficer : _PublServ, IPublicServant
  {
    private bool _hasEmer = false;

    public PoliceOfficer (string name, int age) {
      this.Name = name;
      this.Age  = age;
    }

    //Interface Implementation.
    public string Name {get;set;}
    public int    Age  {get;set;}

    public bool HEmer {
      get {return _hasEmer;}
      set {_hasEmer = value;}
    }

    public override void DTPoI() {
            GIPc ();

      if (this.HEmer)
              TOS();

      FDs();
    }

    private void GIPc() {}
    private void TOS() {}
    private void FDs() {}
  }
}