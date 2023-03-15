using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace APP
{
    public partial class App : Form
    {
        public struct APPFunc
        {
            public string strAPPName;
            public string strAPPFunc;
        };

        public struct Role
        {
            public string strRoleName;
            public string strPermissionsName;
        };

        public App()
        {
            InitializeComponent();
        }

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        public List<APPFunc> listApp = new List<APPFunc>();
        public List<Role> listRole = new List<Role>();

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

        private void buttonGetAllAppFun_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            string strAppName = "";
            string strFunofAppName = "";
            comboBoxAPPName.Items.Clear();
            listApp.Clear();

            if (axCZKEM1.GetAllAppFun (iMachineNumber, out strAppName, out strFunofAppName))
            {
                string[] separatingChars = { "\r\n" };
                strAppName = strAppName.Remove(0, strAppName.IndexOf("\r\n"));
                string[] tmpAppName = strAppName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpAppName)
                {
                    comboBoxAPPName.Items.Add(word);
                }

                strFunofAppName = strFunofAppName.Remove(0, strFunofAppName.IndexOf("\r\n"));
                string[] tmpFuncName = strFunofAppName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpFuncName)
                {
                    string[] separatingChars2 = { "," };
                    string[] tmpAppFunc = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                    APPFunc tmpFun;
                    tmpFun.strAPPFunc = tmpAppFunc[0];
                    tmpFun.strAPPName = tmpAppFunc[1];

                    listApp.Add(tmpFun);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        private void comboBoxAPPName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strAPPName = comboBoxAPPName.Text.Trim();

            if (strAPPName == "")
            {
                MessageBox.Show("Please select app name first", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            comboBoxFuncName.Items.Clear();

            foreach (var app in listApp)
            {
                if (app.strAPPFunc == strAPPName)
                {
                    comboBoxFuncName.Items.Add(app.strAPPName);
                }
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetAllRole_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            string strRoleName = "";

            comboBoxRoleName.Items.Clear();
            comboBoxPermissionsname.Items.Clear();
            listRole.Clear();

            if (axCZKEM1.GetAllRole(iMachineNumber, out strRoleName))
            {
                string[] separatingChars = { "\r\n" };

                strRoleName = strRoleName.Remove(0, strRoleName.IndexOf("\r\n"));
                string[] tmpRoleName = strRoleName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpRoleName)
                {
                    string[] separatingChars2 = { "," };
                    string[] tmpRole = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                    Role role;
                    role.strRoleName = tmpRole[0];
                    role.strPermissionsName = tmpRole[1];

                    listRole.Add(role);

                    comboBoxRoleName.Items.Add(tmpRole[0]);
                    comboBoxPermissionsname.Items.Add(tmpRole[1]);
                    
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        private void comboBoxPermissionsname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermissionsname = comboBoxPermissionsname.Text.Trim();

            if (strPermissionsname == "")
            {
                MessageBox.Show("Please select premission name first", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            comboBoxRoleName.Items.Clear();

            foreach (var role in listRole)
            {
                if (role.strPermissionsName == strPermissionsname)
                {
                    comboBoxRoleName.Items.Add(role.strRoleName);
                }
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetAppOfRole_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermissionsname = comboBoxPermissionsname.Text.Trim();

            if (strPermissionsname == "")
            {
                MessageBox.Show("Please select premission name first", "Error");
                return;
            }

            int idwErrorCode = 0;
            string strAppName = "";

            Cursor = Cursors.WaitCursor;
            comboBoxAPPOfRole.Items.Clear();
            listApp.Clear();

            string strFunofAppName = "";

            if (axCZKEM1.GetAllAppFun(iMachineNumber, out strAppName, out strFunofAppName))
            {
                string[] separatingChars = { "\r\n" };
                strFunofAppName = strFunofAppName.Remove(0, strFunofAppName.IndexOf("\r\n"));
                string[] tmpFuncName = strFunofAppName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpFuncName)
                {
                    string[] separatingChars2 = { "," };
                    string[] tmpAppFunc = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                    APPFunc tmpFun;
                    tmpFun.strAPPFunc = tmpAppFunc[0];
                    tmpFun.strAPPName = tmpAppFunc[1];

                    listApp.Add(tmpFun);
                }
            }

            if (axCZKEM1.GetAppOfRole(iMachineNumber, Convert.ToInt32(strPermissionsname), out strAppName))
            {
                string[] separatingChars = { "\r\n" };

                strAppName = strAppName.Remove(0, strAppName.IndexOf("\r\n"));
                string[] tmpRoleName = strAppName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpRoleName)
                {
                    comboBoxAPPOfRole.Items.Add(word);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            
            Cursor = Cursors.Default;
        }

        private void buttonGetFunOfRole_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermissionsname = comboBoxPermissionsname.Text.Trim();

            if (strPermissionsname == "")
            {
                MessageBox.Show("Please select premission name first", "Error");
                return;
            }

            int idwErrorCode = 0;
            string strFuncName = "";

            Cursor = Cursors.WaitCursor;
            comboBoxFuncOfRole.Items.Clear();

            listApp.Clear();

            string strFunofAppName = "";

            if (axCZKEM1.GetAllAppFun(iMachineNumber, out strFuncName, out strFunofAppName))
            {
                string[] separatingChars = { "\r\n" };
                strFunofAppName = strFunofAppName.Remove(0, strFunofAppName.IndexOf("\r\n"));
                string[] tmpFuncName = strFunofAppName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpFuncName)
                {
                    string[] separatingChars2 = { "," };
                    string[] tmpAppFunc = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                    APPFunc tmpFun;
                    tmpFun.strAPPFunc = tmpAppFunc[0];
                    tmpFun.strAPPName = tmpAppFunc[1];

                    listApp.Add(tmpFun);
                }
            }

            if (axCZKEM1.GetFunOfRole(iMachineNumber, Convert.ToInt32(strPermissionsname), out strFuncName))
            {
                string[] separatingChars = { "\r\n" };

                strFuncName = strFuncName.Remove(0, strFuncName.IndexOf("\r\n"));
                string[] tmpFuncName = strFuncName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in tmpFuncName)
                {
                    comboBoxFuncOfRole.Items.Add(word);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        private void buttonSetPermOfAppFun_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermision = comboBoxPermissionsname.Text.Trim();
            if (strPermision == "")
            {
                MessageBox.Show("Please select permision name first", "Error");
                return;
            }

            string strAppName = comboBoxAPPOfRole.Text.Trim();

            if (strAppName == "")
            {
                MessageBox.Show("Please select app of role first", "Error");
                return;
            }

            string strFuncName = comboBoxFuncOfRole.Text.Trim();

            if (strFuncName == "")
            {
                MessageBox.Show("Please select func of role first", "Error");
                return;
            }

            bool bFind = false;
            foreach (var app in listApp)
            {
                if (app.strAPPFunc == strFuncName && app.strAPPName == strAppName)
                {
                    bFind = true;
                    break;
                }
            }

            if (!bFind)
            {
                MessageBox.Show("Please select correct fun belong to APP", "Error");
                return;
            }

            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;

            if (axCZKEM1.SetPermOfAppFun(iMachineNumber, Convert.ToInt32(strPermision), strAppName, strFuncName))
            {
                axCZKEM1.RefreshData(iMachineNumber);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        private void buttonDeletePermOfAppFun_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermision = comboBoxPermissionsname.Text.Trim();
            if (strPermision == "")
            {
                MessageBox.Show("Please select permision name first", "Error");
                return;
            }

            string strAppName = comboBoxAPPOfRole.Text.Trim();

            if (strAppName == "")
            {
                MessageBox.Show("Please select app of role first", "Error");
                return;
            }

            string strFuncName = comboBoxFuncOfRole.Text.Trim();

            if (strFuncName == "")
            {
                MessageBox.Show("Please select func of role first", "Error");
                return;
            }

            bool bFind = false;
            foreach (var app in listApp)
            {
                if (app.strAPPFunc == strFuncName && app.strAPPName == strAppName)
                {
                    bFind = true;
                    break;
                }   
            }

            if (!bFind)
            {
                MessageBox.Show("Please select correct fun belong to APP", "Error");
                return;
            }

            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;

            if (axCZKEM1.DeletePermOfAppFun(iMachineNumber, Convert.ToInt32(strPermision), strAppName, strFuncName))
            {
                axCZKEM1.RefreshData(iMachineNumber);
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

        private void buttonIsUserDefRoleEnable_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            string strPermision = comboBoxPermissionsname.Text.Trim();
            if (strPermision == "")
            {
                MessageBox.Show("Please select permision name first", "Error");
                return;
            }

            int idwErrorCode = 0;
            bool bEnable = false;

            Cursor = Cursors.WaitCursor;

            if (axCZKEM1.IsUserDefRoleEnable(iMachineNumber, Convert.ToInt32(strPermision), out bEnable))
            {
                if (bEnable)
                {
                    MessageBox.Show("Permision " + strPermision + " is enable", "Sucess");
                }
                else
                {
                    MessageBox.Show("Permision " + strPermision + " is disable", "Sucess");
                }
                
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            Cursor = Cursors.Default;
        }

    }
}
