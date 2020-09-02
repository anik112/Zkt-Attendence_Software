using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZktAttendence.Utilitis;
using System.Xml;
using System.IO;
using ZktAttendence.Core;

namespace ZktAttendence.view
{
    class ConsoleViewV2
    {
        private String zktFilePath;
        private String setupPath;

        public ConsoleViewV2(String zktSetupPath, String setupPath)
        {
            this.zktFilePath = zktSetupPath;
            this.setupPath = setupPath;
        }

        public void showConsole()
        {
            // Print Header text
            //Console.SetWindowSize(100, 80);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******************************************");
            Console.WriteLine("###### Vistasoft IT Bangladesh Ltd. ######");
            Console.WriteLine("######                              ######");
            Console.WriteLine("######     [0] Exit the system      ######");
            Console.WriteLine("******************************************\n");

            // check data store or not
            if (new AttendenceDataWriteInTxt().consoleProcessForAttendence(zktFilePath))
            {
                Console.ForegroundColor = ConsoleColor.Green;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                Console.WriteLine("\n\n*****************************************\n" +
                                 "           Data store in FILE            " +
                                 "\n*****************************************");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }
            else
            {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n*****************************************\n" +
                                    "       Data not store in FILE      " +
                                    "\n*****************************************");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Read();
        }

    }
}
