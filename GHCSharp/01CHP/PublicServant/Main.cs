//Author:
//Date: 05FEB2020
//Essentially this is header to Main.cs
using System;

namespace  _publicservant
{
  public class MClass {
    public static void Main (string[] args) {
      Firefighter firefighter = new FireFighter ("Joe Carrington", 35);
      firefighter.Pension = 5000;

      PrntNameAge(firefighter); //Prototype for printing Name & age
      PrntPension(firefighter); // Prototype for printing Pension Amount

      firefighter.DTPoI();

      PoliceOfficer officer = new PoliceOfficer ("Jane Hope", 32);
      officer.Pension = 5500;
      officer.HEmer = true;

      PrntNameAge(officer);
      PrntNameAge(officer);

      officer.DTPoI();
    }

    static void PrntNameAge (IPublicServant person) {
            Console.WriteLine("Name: " + person.Name);
            Console.WriteLine("Age: "  + person.Age);

    }

    static void PrntPension (_PublServ servant) {
      if (servant is FireFighter)
              Console.WriteLine("Pension of firefighter: " + servant.Pension);
      if (servant is PoliceOfficer)
              Console.WriteLine("Pension of officer: " + servant.Pension);
    }
  }
}
