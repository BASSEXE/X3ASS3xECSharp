//Author: X3ASSEXE
// Title:HelloWorld.cs
//Summary: Simple HelloWorld Program
using System;

namespace HelloWorld
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string HWorld = "Hello World!";
            DateTime DNow = DateTime.Now;
            Console.Write(HWorld);
            Console.WriteLine("The date is " + DNow.ToLongDateString());
        }
    }
}