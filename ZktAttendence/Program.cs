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


            String xmlPath = "\\C#_Project\\ZktAttendence\\Utilitis\\DatabaseSetup.xml";
            XmlDocument xmlDocument = new XmlDocument();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlPath, System.Text.Encoding.UTF8);
            xmlTextWriter.Formatting = Formatting.Indented;

            xmlTextWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");

            xmlTextWriter.WriteStartElement("Vistasoft");
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.Close();

        } 
    }
}

