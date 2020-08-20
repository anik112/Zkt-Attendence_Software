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
