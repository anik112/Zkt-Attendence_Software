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
    class ConsoleView
    {
        private String courser = "0";
        private String zktFilePath;
        private String setupPath;

        public ConsoleView(String zktSetupPath, String setupPath)
        {
            this.zktFilePath = zktSetupPath;
            this.setupPath = setupPath;
        }

        private void showConsoleHeader()
        {
            // Print Header text
            //Console.SetWindowSize(100, 80);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******************************************");
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("###### Vistasoft IT Bangladesh Ltd. ######");
                    Console.WriteLine("******************************************");
                }
                if (i == 1)
                {
                    Console.WriteLine("###### [1] Setup ZKT Machine Info   ######");
                }
                if (i == 2)
                {
                    Console.WriteLine("###### [2] Get ZKT Attendence       ######");
                }
                /*if (i == 3)
                {
                    Console.WriteLine("###### [3] Update ZKT Path          ######");
                }*/
                if (i == 3)
                {
                    Console.WriteLine("###### [0] Exit the system          ######");
                }
            }
            Console.WriteLine("******************************************\n");
            courser = "404";
        }


        public void showMainConsole()
        {
            
            showConsoleHeader();

            bool loopExitChecker = true;
            while (loopExitChecker)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type Command: ");
                // get courser input
                courser = Console.ReadLine();
                Console.Write("\n");

                switch (courser)
                {
                    case "1":
                        try
                        {
                            XmlTextWriter xmlTextWriter = new XmlTextWriter(zktFilePath, System.Text.Encoding.UTF8);
                            xmlTextWriter.WriteStartDocument(true);
                            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
                            xmlTextWriter.WriteStartElement("deviceSetupInfo");

                            int checker = 0;
                            while (true)
                            {
                                Console.WriteLine("Device " + checker);
                                xmlTextWriter.WriteStartElement("device" + checker);
                                //-------------------------------------
                                Console.Write("Machine No: ");
                                String machineNo = Console.ReadLine();
                                //---------------------------------------
                                Console.Write("IpAddress: ");
                                String ipAddress = Console.ReadLine();
                                //---------------------------------------
                                Console.Write("Port: ");
                                String port = Console.ReadLine();
                                //----------------------------------------
                                new SetupUtility().writeMachineInfoInXML(Convert.ToInt32(machineNo), ipAddress, Convert.ToInt32(port), xmlTextWriter);

                                xmlTextWriter.WriteEndElement();
                                checker++;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("[0] for back main menu: ");
                                if (Console.ReadLine().Equals("0"))
                                {
                                    Console.Clear();
                                    showConsoleHeader();
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            xmlTextWriter.WriteEndElement();
                            xmlTextWriter.Close();
                            Console.WriteLine("Data save in file..(-_-)");
                        }catch(Exception e)
                        {
                            Console.WriteLine("\n" + e.Message);
                        }
                        break;


                    case "2":

                        String tempFromDate = String.Empty;
                        String tempToDate = String.Empty;
                        String workToDate = String.Empty; // declare to date variable
                        String workFromDate = String.Empty; // decalre from date variable

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


                        if (new AttendenceDataWriteInTxt().consoleProcessForAttendence(zktFilePath,workFromDate,workToDate))
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
                        Console.Write("[0] for back main menu: ");
                        if (Console.ReadLine().Equals("0"))
                        {
                            Console.Clear();
                            showConsoleHeader();
                        }
                        break;

                    case "-":
                        //new SetupUtility().writeZktFileLoc(setupPath, Console.ReadLine());
                        break;
                      
                    case "0":
                        loopExitChecker = false;
                        break;

                }
            }

        }

    }
}
