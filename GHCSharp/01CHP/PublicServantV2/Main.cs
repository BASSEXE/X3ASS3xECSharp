using System;
namespace PublicServantV2
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            FireFigh _firefigh = new FireFigh("John Carrington", 35);
            _firefigh.PENSA = 5000;

            PrinNameaAge(_firefigh);

            _firefigh.DTPOI();

            PoliOffi _polioffi = new PoliOffi("Jane Hope", 32);
            _polioffi.PENSA = 5500;

            PrinNameaAge(_polioffi);
            PrinPENSA(_polioffi);
             _polioffi.DTPOI();

             _polioffi = new PoliOffi("John Valor",32,true);
             PrinNameaAge(_polioffi);
             _polioffi.DTPOI();
        }

        static void PrinNameaAge (IPUBLSERV person)
        {
          Console.WriteLine("Name: " +person.Name);
          Console.WriteLine("Age: " +person.Age);
        }

        static void PrinPENSA(PublicServantV2 servant)
        {
          if (servant is FireFigh)
          Console.WriteLine("Pension of Firefighter: " + servant.PENSA);
          else if (servant is PoliOffi)
          Console.WriteLine("Pension of officer: "+servant.PENSA);
        }
    }
}
