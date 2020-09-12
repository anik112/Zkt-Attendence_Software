using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

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
                Console.WriteLine("SetupUtility sys: " + e.Message+" > "+ e.StackTrace);
            }
        }

        public void writeMachineInfoInXML(int machineNo, String ipAddress, int port, XmlTextWriter xmlTextWriter)
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
                xmlTextWriter.WriteEndElement();

            }
            catch (Exception e)
            {
                Console.WriteLine("SetupUtility sys: " + e.Message);
            }
        }

        public void writeZktFileLoc(String lodingPath, String storePath)
        {
            try
            {
                XElement element = XElement.Load(lodingPath);
                element.Add(new XElement("zktfilepath", storePath));
                element.Save(lodingPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("SetupUtility sys: " + e.Message);
            }
        }

        public String getZktFilePath(String lodingPath)
        {
            String path = "No path";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(lodingPath);
                XmlNode node = xmlDocument.SelectSingleNode("setup/zktfilepath");
                Console.WriteLine(node.ChildNodes);
                path = node.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine("SetupUtility sys: " + e.Message);
            }
            return path;
        }


        public List<MachineSelector> getDeviceSetupInformation(String filePath, String rootNode)
        {
            List<MachineSelector> machineList = new List<MachineSelector>();
            try
            {
                // read xml file
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                XmlNodeList nodeList = xmlDocument.SelectNodes($"/{rootNode}");

                int nodeCount = 0;
                while(true)
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
                    }

                    machineList.Add(machineSelector);
                    nodeCount++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("SetupUtility sys: " + e.Message+" > "+ e.StackTrace);
                Console.ReadLine();
            }
            return machineList;
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
                Console.WriteLine("SetupUtility sys: " + e.Message);
                Console.ReadLine();
            }
            return null;
        }

    }
}
