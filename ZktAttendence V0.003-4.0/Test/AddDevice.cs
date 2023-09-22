using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Test
{
    public partial class AddDevice : Form
    {

        private CheckBox[] box;
        private String filePath="";
        private List<MachineSelector> getStroedMachineList = new List<MachineSelector>();

        public AddDevice(String path)
        {
            InitializeComponent();
            this.filePath = path;
            this.getStroedMachineList =  new SetupUtility().getDeviceSetupInformation(filePath, "deviceSetupInfo"); // get all device info in array
            viewMachineList(filePath, getStroedMachineList);
        }

        private void btnAddDevice_Click(object sender, EventArgs e)
        {

            String machineNo = txtMachineNo.Text;
            String ipAddress = txtIpAddress.Text;
            String password = txtPassword.Text;
            String location = txtLocation.Text;

            MachineSelector selector = new MachineSelector();
            selector.setMachineNumber(int.Parse(machineNo));
            selector.setIpAddress(ipAddress);
            selector.setcomPass(int.Parse(password));
            selector.setAddress(location);

            //getStroedMachineList = new SetupUtility().getDeviceSetupInformation(filePath, "deviceSetupInfo"); // get all device info in array
            List<MachineSelector> machineList = new List<MachineSelector>();

            if (getStroedMachineList.Count > 0)
            {
                machineList.Add(selector);
                getStroedMachineList.AddRange(machineList);
            }
            else
            {
                getStroedMachineList.Add(selector);
            }

            addMachineList(getStroedMachineList, filePath);
            clearData();
            viewMachineList(filePath,getStroedMachineList);

        }

        private void AddDevice_Activated(object sender, EventArgs e)
        {

        }

        private void clearData()
        {
            txtMachineNo.Text = "";
            txtIpAddress.Text = "";
            txtLocation.Text = "";
            txtPassword.Text = "";
            showPanel.Controls.Clear();
        }

        private void addMachineList(List<MachineSelector> machineList, String filePath)
        {
            XmlWriter writer = XmlWriter.Create(filePath);

            int i = 0;

            writer.WriteStartElement("deviceSetupInfo");
            foreach (MachineSelector s in machineList)
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


        private void viewMachineList(String zktFilePath, List<MachineSelector> list)
        {
            showPanel.Controls.Clear();
            //List<MachineSelector> getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array
            box = new CheckBox[list.Count];
            int i = 0;
            int deviceCount = 1;
            foreach (MachineSelector selector in list)
            {
                box[i] = new CheckBox();
                box[i].AutoSize = true;
                box[i].Cursor = System.Windows.Forms.Cursors.Hand;
                //box[i].BackColor = System.Drawing.Color.LightSkyBlue;
                box[i].Location = new System.Drawing.Point(5, i * 25);
                box[i].Name = selector.getIpAddress();
                box[i].Size = new System.Drawing.Size(80, 17);
                box[i].Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
                box[i].Font = new Font(FontFamily.GenericMonospace, 9f);
                //box[i].ForeColor = Color.DarkMagenta;
                box[i].Text = deviceCount.ToString().PadLeft(2) + " | " + selector.getIpAddress() + " - " + selector.getAddress() + " - " + selector.getMachineNumber();
                box[i].UseVisualStyleBackColor = false;
                box[i].TextAlign = ContentAlignment.MiddleLeft;
                box[i].CheckAlign = ContentAlignment.MiddleLeft;
                showPanel.Controls.Add(box[i]);
                i++;
                deviceCount++;
            }
        }
    }
}
