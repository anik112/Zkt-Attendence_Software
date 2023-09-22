using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private String workToDate = String.Empty; // declare to date variable
        private String workFromDate = String.Empty; // decalre from date variable

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


            String tempFromDate = String.Empty;
            String tempToDate = String.Empty;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                // get date from user
                Console.Write("From Date: ");
                tempFromDate = Console.ReadLine();
                // get date from user
                Console.Write("To Date: ");
                tempToDate = Console.ReadLine();
                // check given date is valid or not
                if (tempFromDate.Length == 8 && tempToDate.Length == 8)
                {
                    // Formating given data into Day, Month, Year
                    int checkDayOfFromDate = Convert.ToInt32(tempFromDate.Substring(0, 2));
                    int checkMOnthOfFromDate = Convert.ToInt32(tempFromDate.Substring(2, 2));
                    int checkYearOfFromDate = Convert.ToInt32(tempFromDate.Substring(4, 4));
                    int checkDayOfToDate = Convert.ToInt32(tempToDate.Substring(0, 2));
                    int checkMonthOfToDate = Convert.ToInt32(tempToDate.Substring(2, 2));
                    int checkYearOfToDate = Convert.ToInt32(tempToDate.Substring(4, 4));

                    if ((checkDayOfFromDate >= 1 && checkDayOfFromDate <= 31)
                        && (checkMOnthOfFromDate >= 1 && checkMOnthOfFromDate <= 12)
                        && (checkYearOfFromDate >= 2000 && checkYearOfFromDate <= 3000)
                        && (checkDayOfToDate >= 1 && checkDayOfToDate <= 31)
                        && (checkMonthOfToDate >= 1 && checkMonthOfToDate <= 12)
                        && (checkYearOfToDate >= 2000 && checkYearOfToDate <= 3000))
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n=> Sorry Date is not valid...\n");
                    }
                }
                // If data isn't valid
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n=> Sorry Date is not valid...\n");
                }
            }
            // make final format of date
            workFromDate = tempFromDate.Substring(2, 2) + "/" + tempFromDate.Substring(0, 2) + "/" + tempFromDate.Substring(4, 4);
            // make final format of date
            workToDate = tempToDate.Substring(2, 2) + "/" + tempToDate.Substring(0, 2) + "/" + tempToDate.Substring(4, 4);
            // Show massage
            Console.WriteLine("\n------------------------- \n " + workFromDate + " to " + workToDate + "\n------------------------- \n");

            // check data store or not
            if (new AttendenceDataWriteInTxt().consoleProcessForAttendence(zktFilePath, workFromDate, workToDate))
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
