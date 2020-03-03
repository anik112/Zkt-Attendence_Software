using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZktAttendence.Core_Service;
using ZktAttendence.Core;
using zkemkeeper;
using ZktAttendence.Utilitis;
using System.Xml;
using System.IO;

namespace ZktAttendence
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Master().DriverMethod();
            //new UpdateInDatabase().getUserInfoFromDatabase(DatabaseConnection.getConnection());
            /*new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\n#########  Please type Enter & Close  ###########" +
                                "\n@ 2019-Vistasoft IT Bangladesh Ltd. Dev-by-Pranta");
            Console.ReadLine();*/


            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\Setup.xml");
            // write xml file
            String filePath = "\\C#_Project\\ZktAttendence\\Setup.xml";
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument(true);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartElement("setup_database");
            new SetupUtility().xmlWriter("DESKTOP-NSLL7T5", "payroll", "payroll", "payroll", xmlTextWriter);
            new SetupUtility().xmlWriter("DESKTOP-NSLL7T5", "payroll2", "payroll2", "payroll2", xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();

        }
    }
}

