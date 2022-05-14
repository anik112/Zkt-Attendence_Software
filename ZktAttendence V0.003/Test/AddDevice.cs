using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Test
{
    public partial class AddDevice : Form
    {

        private CheckBox[] box;

        public AddDevice()
        {
            InitializeComponent();
        }

        private void btnAddDevice_Click(object sender, EventArgs e)
        {

        }

        private void AddDevice_Activated(object sender, EventArgs e)
        {

        }


        private void viewMachineList(String zktFilePath)
        {
            showPanel.Controls.Clear();
            List<MachineSelector> getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array
            box = new CheckBox[getMachineList.Count];
            int i = 0;
            int deviceCount = 1;
            foreach (MachineSelector selector in getMachineList)
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
                tablePan.Controls.Add(box[i]);
                i++;
                deviceCount++;
            }
        }
    }
}
