using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bell
{
    public partial class Bell : Form
    {
        public struct BellInfo
        {
            public int iWeekDay;
            public int iIndex;
            public int iEnable;
            public int iHour;
            public int iMin;
            public int iVoice;
            public int iWay;
            public int iInerBellDelay;
            public int iExtBellDelay;
        };

        public Bell()
        {
            InitializeComponent();
        }

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        private List<BellInfo> bellInfo = new List<BellInfo>();

        /********************************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.                                           *
        * This part is for demonstrating the communication with your device.There are 3 communication ways: "TCP/IP","Serial Port" and "USB Client".*
        * The communication way which you can use duing to the model of the device.                                                                 *
        * *******************************************************************************************************************************************/
        #region Communication
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.

        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
            {
                MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (btnConnect.Text == "DisConnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btnConnect.Text = "Connect";
                lblState.Text = "Current State:DisConnected";
                Cursor = Cursors.Default;

                comboBoxIndex.Items.Clear();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                comboBoxIndex.Text = "";
                comboBoxBellWay.SelectedIndex = -1;
                comboBoxEnable.SelectedIndex = -1;
                textBoxExtBellDelay.Text = "";
                textBoxHour.Text = "";
                textBoxInerBellDelay.Text = "";
                textBoxMinute.Text = "";
                textBoxVoice.Text = "";

                return;
            }

            bIsConnected = axCZKEM1.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == true)
            {
                btnConnect.Text = "DisConnect";
                btnConnect.Refresh();
                lblState.Text = "Current State:Connected";
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //If your device supports the SerialPort communications, you can refer to this.
        private void btnRsConnect_Click(object sender, EventArgs e)
        {
            if (cbPort.Text.Trim() == "" || cbBaudRate.Text.Trim() == "" || txtMachineSN.Text.Trim() == "")
            {
                MessageBox.Show("Port,BaudRate and MachineSN cannot be null", "Error");
                return;
            }
            int idwErrorCode = 0;
            //accept serialport number from string like "COMi"
            int iPort;
            string sPort = cbPort.Text.Trim();
            for (iPort = 1; iPort < 10; iPort++)
            {
                if (sPort.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            Cursor = Cursors.WaitCursor;
            if (btnRsConnect.Text == "Disconnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btnRsConnect.Text = "Connect";
                btnRsConnect.Refresh();
                lblState.Text = "Current State:Disconnected";
                Cursor = Cursors.Default;
                return;
            }

            iMachineNumber = Convert.ToInt32(txtMachineSN.Text.Trim());//when you are using the serial port communication,you can distinguish different devices by their serial port number.
            bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, Convert.ToInt32(cbBaudRate.Text.Trim()));

            if (bIsConnected == true)
            {
                btnRsConnect.Text = "Disconnect";
                btnRsConnect.Refresh();
                lblState.Text = "Current State:Connected";

                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        //If your device supports the USBCLient, you can refer to this.
        //Not all series devices can support this kind of connection.Please make sure your device supports USBClient.
        //Connect the device via the virtual serial port created by USBClient
        private void btnUSBConnect_Click(object sender, EventArgs e)
        {
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;

            if (btnUSBConnect.Text == "Disconnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btnUSBConnect.Text = "Connect";
                btnUSBConnect.Refresh();
                lblState.Text = "Current State:Disconnected";
                Cursor = Cursors.Default;
                return;
            }

            SearchforUSBCom usbcom = new SearchforUSBCom();
            string sCom = "";
            bool bSearch = usbcom.SearchforCom(ref sCom);//modify by Darcy on Nov.26 2009
            if (bSearch == false)//modify by Darcy on Nov.26 2009
            {
                MessageBox.Show("Can not find the virtual serial port that can be used", "Error");
                Cursor = Cursors.Default;
                return;
            }

            int iPort;
            for (iPort = 1; iPort < 10; iPort++)
            {
                if (sCom.IndexOf(iPort.ToString()) > -1)
                {
                    break;
                }
            }

            iMachineNumber = Convert.ToInt32(txtMachineSN2.Text.Trim());
            if (iMachineNumber == 0 || iMachineNumber > 255)
            {
                MessageBox.Show("The Machine Number is invalid!", "Error");
                Cursor = Cursors.Default;
                return;
            }

            int iBaudRate = 115200;//115200 is one possible baudrate value(its value cannot be 0)
            bIsConnected = axCZKEM1.Connect_Com(iPort, iMachineNumber, iBaudRate);

            if (bIsConnected == true)
            {
                btnUSBConnect.Text = "Disconnect";
                btnUSBConnect.Refresh();
                lblState.Text = "Current State:Connected";
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        #endregion

        private void buttonReadAllBellSchData_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            comboBoxIndex.Items.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            comboBoxIndex.Text = "";
            comboBoxBellWay.SelectedIndex = -1;
            comboBoxEnable.SelectedIndex = -1;
            textBoxExtBellDelay.Text = "";
            textBoxHour.Text = "";
            textBoxInerBellDelay.Text = "";
            textBoxMinute.Text = "";
            textBoxVoice.Text = "";

            int idwErrorCode = 0;

            int iWeekDay = 0;
		    int iIndex = 0;
		    int iEnable = 0; 
		    int iHour = 0;
		    int iMin = 0; 
		    int iVoice = 0; 
		    int iWay = 0; 
		    int iInerBellDelay = 0; 
		    int iExtBellDelay = 0;


            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ReadAllBellSchData(iMachineNumber))
            {
                while (axCZKEM1.GetEachBellInfo(iMachineNumber, out iWeekDay, out iIndex, out iEnable, out iHour, out iMin, out iVoice, out iWay, out iInerBellDelay, out iExtBellDelay))
                {
                    BellInfo tmpInfo = new BellInfo();
                    tmpInfo.iWeekDay = iWeekDay;
                    tmpInfo.iIndex = iIndex;
                    tmpInfo.iEnable = iEnable;
                    tmpInfo.iHour = iHour;
                    tmpInfo.iMin = iMin;
                    tmpInfo.iVoice = iVoice;
                    tmpInfo.iWay = iWay;
                    tmpInfo.iInerBellDelay = iInerBellDelay;
                    tmpInfo.iExtBellDelay = iExtBellDelay;

                    bellInfo.Add(tmpInfo);

                    comboBoxIndex.Items.Add(iIndex);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetDayBellSchCount_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            int idwErrorCode = 0;

            int iDayBellCnt  = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetDayBellSchCount(iMachineNumber, out iDayBellCnt))
            {
                txtShow.Text = iDayBellCnt.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetMaxBellIDInBellSchData_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            int idwErrorCode = 0;

            int MaxBellID = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetMaxBellIDInBellSchData(iMachineNumber, out MaxBellID))
            {
                txtShow.Text = MaxBellID.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetBellSchDataEx_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (comboBoxIndex.Text.Trim() == "")
            {
                MessageBox.Show("Please select index first", "Error");
                return;
            }

            int idwErrorCode = 0;

            int iWeekDay = 0;

            if (checkBox1.Checked)
            {
                iWeekDay = 0;
            }
            else if (checkBox2.Checked)
            {
                iWeekDay = 1;
            }
            else if (checkBox3.Checked)
            {
                iWeekDay = 2;
            }
            else if (checkBox4.Checked)
            {
                iWeekDay = 3;
            }
            else if (checkBox5.Checked)
            {
                iWeekDay = 4;
            }
            else if (checkBox6.Checked)
            {
                iWeekDay = 5;
            }
            else if (checkBox7.Checked)
            {
                iWeekDay = 6;
            }
            
            int iIndex = Convert.ToInt32(comboBoxIndex.Text.Trim());
            int iEnable = 0;
            int iHour = 0;
            int iMin = 0;
            int iVoice = 0;
            int iWay = 0;
            int iInerBellDelay = 0;
            int iExtBellDelay = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetBellSchDataEx(iMachineNumber, iWeekDay, iIndex, out iEnable, out iHour, out iMin, out iVoice, out iWay, out iInerBellDelay, out iExtBellDelay))
            {
                comboBoxEnable.Text = iEnable.ToString();
                textBoxHour.Text = iHour.ToString();
                textBoxMinute.Text = iMin.ToString();
                textBoxVoice.Text = iVoice.ToString();
                if (iEnable == 1)
                {
                    comboBoxEnable.Text = "true";
                }
                else
                {
                    comboBoxEnable.Text = "false";
                }

                comboBoxBellWay.SelectedIndex = iWay;

                textBoxInerBellDelay.Text = iInerBellDelay.ToString();
                textBoxExtBellDelay.Text = iExtBellDelay.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonSetBellSchDataEx_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }


            if (comboBoxIndex.Text.Trim() == "")
            {
                MessageBox.Show("Please select index first", "Error");
                return;
            }

            int idwErrorCode = 0;

            int iWeekDay = 0;

            if (checkBox1.Checked)
            {
                iWeekDay += 1;
            }
            if (checkBox2.Checked)
            {
                iWeekDay += 2;
            }
            if (checkBox3.Checked)
            {
                iWeekDay += 4;
            }
            if (checkBox4.Checked)
            {
                iWeekDay += 8;
            }
            if (checkBox5.Checked)
            {
                iWeekDay += 16;
            }
            if (checkBox6.Checked)
            {
                iWeekDay += 32;
            }
            if (checkBox7.Checked)
            {
                iWeekDay += 64;
            }

            int iIndex = Convert.ToInt32(comboBoxIndex.Text.Trim());

            int iEnable = Convert.ToInt32(comboBoxEnable.SelectedIndex);
            int iHour = Convert.ToInt32(textBoxHour.Text.Trim());
            int iMin = Convert.ToInt32(textBoxMinute.Text.Trim());
            int iVoice = 0;
            int iWay = Convert.ToInt32(comboBoxBellWay.SelectedIndex);
            int iInerBellDelay = Convert.ToInt32(textBoxInerBellDelay.Text.Trim());
            int iExtBellDelay = Convert.ToInt32(textBoxExtBellDelay.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SetBellSchDataEx(iMachineNumber, iWeekDay, iIndex, iEnable, iHour, iMin, iVoice, iWay, iInerBellDelay, iExtBellDelay))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("Set bell success!", "Error");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void comboBoxIndex_SelectedIndexChanged(object sender, EventArgs e)
        {

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            int iIndex = Convert.ToInt32(comboBoxIndex.Text.Trim());

            Cursor = Cursors.WaitCursor;
            foreach (var bell in bellInfo)
            {
                if (bell.iIndex == iIndex)
                {
                    int iWeek = bell.iWeekDay;
                    if (iWeek >= 64)
                    {
                        checkBox7.Checked = true;
                        iWeek -= 64;
                    }
                    if (iWeek >= 32)
                    {
                        checkBox6.Checked = true;
                        iWeek -= 32;
                    }
                    if (iWeek >= 32)
                    {
                        checkBox5.Checked = true;
                        iWeek -= 16;
                    }
                    if (iWeek >= 8)
                    {
                        checkBox4.Checked = true;
                        iWeek -= 8;
                    }
                    if (iWeek >= 4)
                    {
                        checkBox3.Checked = true;
                        iWeek -= 4;
                    }
                    if (iWeek >= 2)
                    {
                        checkBox2.Checked = true;
                        iWeek -= 2;
                    }
                    if (iWeek >= 1)
                    {
                        checkBox1.Checked = true;
                        iWeek -= 1;
                    }
                }
            }
            Cursor = Cursors.Default;
        }
    }
}
