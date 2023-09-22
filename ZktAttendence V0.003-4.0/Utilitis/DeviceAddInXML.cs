using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void writeOperation(List<MachineSelector> selectors, String filePath)
        {
            XmlWriter writer = XmlWriter.Create(filePath);

            int i = 0;

            writer.WriteStartElement("deviceSetupInfo");
            foreach (MachineSelector s in selectors)
            {
                writer.WriteStartElement("device" + i);
                writer.WriteElementString("machineNo", s.getMachineNumber().ToString());
                writer.WriteElementString("ipAddress", s.getIpAddress());
                writer.WriteElementString("port", s.getPortNumber().ToString());
                writer.WriteElementString("pass", s.getcomPass().ToString());
                writer.WriteElementString("location", s.getAddress());
                writer.WriteEndElement();
                i++;
            }
            writer.WriteEndElement();
            writer.Flush();

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
