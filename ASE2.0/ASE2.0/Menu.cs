﻿using System;
namespace ASE2._0
{
    public class Menu
    {
        public string MenuDisplay()
        {
            Console.WriteLine(@"ASEScanner By T0g3ly
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,/,,,,,,,,,,///,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,/,,,,*,,,,// /,,,,,,,,/,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,*,,,,,,,/  ,//,,,*/  /,,,/////,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,(&&&&&&&/%&@, *,,/   //// ,/*,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,#(%%%&&&&&&&&&&&&&&&/    /    *,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,(&&&&&&&&&&&&&&&&&&        **/,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,((&&&&&&&&&&&(&.&#&&&       *,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,&&&&&&&&&&&&&&&&&%. ,(((((      //*,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,((((&&@&&&&&(#(((   ((    //,,,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,#(&.%*     &&&&((//,,,,,,,,,,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,*,,((( .. & &&&&&&&&/&&&&&&&,,,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,(&&&&&&&&%//&&&&&&&&&&&,,,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,&&&&&&  &(&&&/&&&&&&%,,,,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,**&&**  /&(/(&&&&&&.&&&&&,,,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,*****// &#/%//,//&(%((((((&,,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,/* ***&&&&&&*//,/*&(&&&&&&&&&,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.****/&**,* //,/////&&&&&&&&@,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,/ *********./,. / ////(&&&&&,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,*.  ***,...        //&//&&&&,,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.*.******            //&&&&&/*,,,,,,,,,,,
,,,,*,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.  ,   *****..        ****** .,,,,,,,,,,,
,,,,,*,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,.        *****              ..,,,,,,,,,,,
,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,. .   ..     ,****    ...,.   ..,,,,,,,,,,,
-----------------------------
|Choose an Option from Below:
----------------------------
|1.|- Test for url for XSS Injection |
|2.|- Test for url for SQL Injection |
|3.|- Enumerate subdirectories. |
|4.|- Send NULL Byte Injection. |
|5.|- Quit application |

");
            string menuResponse = default;
            Start:
            switch (Console.ReadLine())
            {
                case "1":
                    menuResponse = "1";
                    break;
                case "2":
                    menuResponse = "2";
                    break;
                case "3":
                    menuResponse = "3";
                    break;
                case "4":
                    menuResponse = "4";
                    break;
                case "5":
                    Console.WriteLine("Exiting ...");
                    Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("Invalid Choice");
                    DisplayHelp();
                    goto Start;
            }

            return menuResponse;
            void DisplayHelp() => Console.WriteLine(@"
-----------------------------
|Choose an Option from Below:
----------------------------
|1.|- Test for url for XSS Injection |
|2.|- Test for url for SQL Injection |
|3.|- Enumerate subdirectories. |
|4.|- Send NULL Byte Injection. |
|5.|- Quit application |");

        }
    }
}
