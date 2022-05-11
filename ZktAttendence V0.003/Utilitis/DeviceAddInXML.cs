using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZktAttendence.Utilitis
{
    class DeviceAddInXML
    {

        private String filePath;
        private String rootNode;

        public void setFilePathAndRootNode(String path, String node)
        {
            this.filePath = path;
            this.rootNode = node;
        }

        // Work pandding in this function............
        public void writeOperation()
        {
            // read xml file
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            XmlNodeList nodeList = xmlDocument.SelectNodes($"/{rootNode}");

            int nodeCount = 0;
            while (true)
            {
                XmlNodeList innerNode = xmlDocument.SelectNodes($"/{rootNode}/device{nodeCount}");
                if (innerNode.Count <= 0)
                {
                    break;
                }

                MachineSelector machineSelector = new MachineSelector();
                foreach (XmlNode xmlNode in innerNode)
                {
                    machineSelector.setMachineNumber(Convert.ToInt32(xmlNode.SelectSingleNode("machineNo").InnerText));
                    //Console.WriteLine("Machine No: " + machineSelector.getMachineNumber());
                    machineSelector.setIpAddress(xmlNode.SelectSingleNode("ipAddress").InnerText);
                    //Console.WriteLine("Ip address: "+machineSelector.getIpAddress());
                    machineSelector.setPortNumber(Convert.ToInt32(xmlNode.SelectSingleNode("port").InnerText));
                    machineSelector.setcomPass(Convert.ToInt32(xmlNode.SelectSingleNode("pass").InnerText));
                    machineSelector.setAddress(xmlNode.SelectSingleNode("location").InnerText);
                }

                nodeCount++;
            }

        }


        public void writeMachineListInXML(int machineNo, String ipAddress, int port, String password, String location, XmlTextWriter xmlTextWriter)
        {
            try
            {
                xmlTextWriter.WriteStartElement("machineNo");
                xmlTextWriter.WriteString(machineNo.ToString());
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("ipAddress");
                xmlTextWriter.WriteString(ipAddress);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("port");
                xmlTextWriter.WriteString(port.ToString());
                xmlTextWriter.WriteStartElement("pass");
                xmlTextWriter.WriteString(password);
                xmlTextWriter.WriteStartElement("location");
                xmlTextWriter.WriteString(location);
                xmlTextWriter.WriteEndElement();

            }
            catch (Exception e)
            {
                Console.WriteLine("SetupUtility sys: " + e.Message);
            }
        }


    }
}
