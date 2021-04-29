using System;
using System.Net;
using System.DirectoryServices;
using SearchScope = System.DirectoryServices.SearchScope;
//using System.DirectoryServices.ActiveDirectory;
//using System.DirectoryServices.Protocols;


namespace Kibangee_Hound
{
    class KHound
    {
        static void Main(string[] args)
        {
            //...

            DirectoryEntry objADAM;
            DirectoryEntry objGrpEnt;               //GroupResults.
            DirectorySearcher objSrcADAM;            //Search Object
            SearchResultCollection objSrcRes;    //Results Collection

            // Binding Path
            //Construct Binding String
            string strPath = "LDAP://localhost:389/OU=TestOU,O=Fabrikahm,C=US";

            Console.WriteLine("Bind to: {0}", strPath);
            Console.WriteLine("Enum:     Groups and members");

            //Get Object
            try
            {
                objADAM = new DirectoryEntry(strPath);
                objADAM.RefreshCache();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Bind failed.");
                Console.WriteLine("       {0}", e.Message);

                return;
            }

            //Get src object filter / scope
            //perform search.
            try
            {
                objSrcADAM = new DirectorySearcher(objADAM);
                objSrcADAM.Filter = "(&amp;(objectClass=group))";
                objSrcADAM.SearchScope = SearchScope.Subtree;
                objSrcRes = objSrcADAM.FindAll();

            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:   Search Failed;");
                Console.WriteLine("         {0}", e.Message);

                return;
            }

            //Enum Groups and Users.

            try
            {
                if (objSrcRes.Count != 0)
                {
                    foreach (SearchResult objRes in objSrcRes)
                    {
                        objGrpEnt = objRes.GetDirectoryEntry();
                        Console.WriteLine("Group    {0}",
                            objGrpEnt.Name);
                        foreach (object objMem in objGrpEnt.Properties["member"])
                        {
                            Console.WriteLine("Member: {0}", objMem.ToString());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Results: No groups found.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:   Enumeration failed.\n{0}", e.Message);
                return;
            }
            Console.WriteLine("Success: Enumeration Complete");
            //...
        }
    }
}
