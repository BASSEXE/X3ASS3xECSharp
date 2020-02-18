//02172020
//X3A55ExE XSSQLI
// Initial 



using System;
using System.Net; // WebRequest, WebResponse
using System.IO;

namespace XSSQLI {
	  class MCLASS{
		  public static void Main (string[] args) {
			  //string urlT = args[0];
			  string urlT= "site.com?search=test&time=now";
			  string [] paramT = urlT.Remove(0, urlT.IndexOf("?") + 1).Split('&');


			  foreach (string parm in paramT) {
				  string XSS = urlT.Replace(parm, parm + "fd<ssx>sa");
				  string SQL = urlT.Replace(parm, parm + "fd'sa");

				  Console.WriteLine(XSS,SQL);
			  }
		  }
	  }
}