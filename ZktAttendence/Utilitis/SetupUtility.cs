using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZktAttendence.Utilitis
{
    class SetupUtility
    {

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
                Console.WriteLine("SetupUtility sys: "+e.Message);
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
                Console.WriteLine("SetupUtility sys: "+e.Message);
                Console.ReadLine();
            }
            return null;
        }

    }
}
