using System;
using System.Xml;

namespace ZktAttendence.Test
{
    class XMLtest
    {

        public static void main(String[] args)
        {
            // write xml file
            String filePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Setup.xml");
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
            xmlTextWriter.WriteStartDocument(true);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlTextWriter.WriteStartElement("setup_database");
            new XMLtest().xmlWriter("DESKTOP-NSLL7T5", "payroll", "payroll", "payroll", xmlTextWriter);
            new XMLtest().xmlWriter("DESKTOP-NSLL7T5", "payroll2", "payroll2", "payroll2", xmlTextWriter);
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();

            // Read from xml file
            XmlNodeList list = new XMLtest().getDatabaseSetupInformation(filePath, "setup_database", "server_1");
            foreach (XmlNode node in list)
            {
                Console.WriteLine("Host: " + node.SelectSingleNode("host").InnerText);
                Console.WriteLine("Service name: " + node.SelectSingleNode("service_name").InnerText);
                Console.WriteLine("User id: " + node.SelectSingleNode("user_id").InnerText);
                Console.WriteLine("password: " + node.SelectSingleNode("password").InnerText);
                Console.WriteLine("----------------------------------------\n");
            }
            Console.ReadLine();

        }

        public void xmlWriter(String host, String serviceName, String userId, String password, XmlTextWriter xmlTextWriter)
        {
            try
            {
                xmlTextWriter.WriteStartElement("host");
                xmlTextWriter.WriteString(host);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("service_name");
                xmlTextWriter.WriteString(serviceName);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("user_id");
                xmlTextWriter.WriteString(userId);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("password");
                xmlTextWriter.WriteString(password);
                xmlTextWriter.WriteEndElement();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public XmlNodeList getDatabaseSetupInformation(String filePath, String rootNode, String selectedSubNode)
        {
            try
            {
                // read xml file
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                XmlNodeList nodeList = xmlDocument.SelectNodes($"/{rootNode}/{selectedSubNode}");
                return nodeList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
