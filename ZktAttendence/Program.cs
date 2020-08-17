using System;
using System.Drawing.Text;
using System.Xml;
using ZktAttendence.Test;
using ZktAttendence.Utilitis;

namespace ZktAttendence
{
    class Program
    {

        private static String zktFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\SetupMachineList.xml");

        static void Main(string[] args)
        {

            new ZktAttendence.view.ConsoleView(zktFilePath).showMainConsole();
            //Console.ReadLine();
            /* setMachineInfo();
             getMachineInfo();*/
            //new XMLtest();

            //new Master().DriverMethod();
            //new UpdateInDatabase().getUserInfoFromDatabase(DatabaseConnection.getConnection());
            /*
            new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\n#########  Please type Enter & Close  ###########" +
                                "\n@ 2019-Vistasoft IT Bangladesh Ltd. Dev-by-Pranta");
            Console.ReadLine();
            */

           /* // write xml file
            String dbaFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Setup.xml");
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(dbaFilePath, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument(true);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            xmlTextWriter.WriteStartElement("setup_database");
            xmlTextWriter.WriteStartElement("server_1");
            new XMLtest().xmlWriter("DESKTOP-NSLL7T5", "payroll", "payroll", "payroll", xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteStartElement("server_2");
            new XMLtest().xmlWriter("DESKTOP-NSLL7T5", "payroll2", "payroll2-s", "payroll2-s", xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();*/

            /*// Read from xml file
            System.Xml.XmlNodeList list = new XMLtest().getDatabaseSetupInformation(filePath, "setup_database", "server_1");
            foreach (System.Xml.XmlNode node in list)
            {
                Console.WriteLine("Host: " + node.SelectSingleNode("host").InnerText);
                Console.WriteLine("Service name: " + node.SelectSingleNode("service_name").InnerText);
                Console.WriteLine("User id: " + node.SelectSingleNode("user_id").InnerText);
                Console.WriteLine("password: " + node.SelectSingleNode("password").InnerText);
                Console.WriteLine("----------------------------------------\n");
            }
            Console.ReadLine();*/


        }


        public static void setMachineInfo()
        {
            String DEVICE_SETUP_NODE = "deviceSetupInfo";

            System.Xml.XmlTextWriter xmlTextWriter = new XmlTextWriter(zktFilePath, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument(true);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            xmlTextWriter.WriteStartElement(DEVICE_SETUP_NODE);
            xmlTextWriter.WriteStartElement("device01");
            new SetupUtility().writeMachineInfoInXML(101, "192.168.1.201", 1215, xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteStartElement("device02");
            new SetupUtility().writeMachineInfoInXML(102, "192.168.1.202", 1215, xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteStartElement("device03");
            new SetupUtility().writeMachineInfoInXML(103, "192.168.1.203", 1215, xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.Close();
        }

        public static void getMachineInfo()
        {
            String filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\SetupMachineList.xml");
            System.Xml.XmlNodeList list = new XMLtest().getDatabaseSetupInformation(filePath, "deviceSetupInfo", "device01");
            foreach (System.Xml.XmlNode node in list)
            {
                Console.WriteLine("MachineNo: " + node.SelectSingleNode("machineNo").InnerText);
                Console.WriteLine("IpAddress: " + node.SelectSingleNode("ipAddress").InnerText);
                Console.WriteLine("Port: " + node.SelectSingleNode("port").InnerText);
                Console.WriteLine("----------------------------------------\n");
            }
        }
    }
}

