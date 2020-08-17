using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZktAttendence.Utilitis;
using System.Xml;

namespace ZktAttendence.view
{
    class ConsoleView
    {
        private String courser = "0";
        private String zktFilePath;

        public ConsoleView(String zktSetupPath)
        {
            this.zktFilePath = zktSetupPath;
        }

        private void showConsoleHeader()
        {
            // Print Header text
            //Console.SetWindowSize(100, 80);
            Console.WriteLine("******************************************");
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("*      Vistasoft IT Bangladesh Ltd.      *");
                    Console.WriteLine("*                                        *");
                }
                if (i == 1)
                {
                    Console.WriteLine("*      1. Setup ZKT Machine Info         *");
                }
                if (i == 2)
                {
                    Console.WriteLine("*      2. Get ZKT Attendence             *");
                }
                if (i == 3)
                {
                    Console.WriteLine("*      0. Exit the system                *");
                }
            }
            Console.WriteLine("******************************************");
            courser = "404";
        }


        public void showMainConsole()
        {
            showConsoleHeader();

            bool loopExitChecker = true;
            while (loopExitChecker)
            {
                // get courser input
                courser = Console.ReadLine();

                switch (courser)
                {
                    case "1":
                        List<XmlNodeList> xmlNodes = new List<System.Xml.XmlNodeList>();
                        System.Xml.XmlTextWriter xmlTextWriter = new XmlTextWriter(zktFilePath, System.Text.Encoding.UTF8);
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
                            Console.WriteLine("For Exit Type: 0");
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
                        break;


                    case "2":


                    case "0":
                        loopExitChecker = false;
                        break;

                }
            }

        }

    }
}
