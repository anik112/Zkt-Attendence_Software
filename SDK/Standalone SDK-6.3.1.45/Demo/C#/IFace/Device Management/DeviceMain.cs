/**********************************************************
 * Demo for Standalone SDK.Created by Darcy on Oct.15 2009*
***********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace Device
{
    public partial class DeviceMain : Form
    {
        public DeviceMain()
        {
            InitializeComponent();
        }

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        /*************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
        * This part is for demonstrating the communication with your device.                             *
        * ************************************************************************************************/
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

        #endregion

        /********************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.                               *
        * This part is for demonstrating the operations of the device information, status, options and other Frequently used  functions.*
        * In this part, there are lots of parameters involved, please refer to development manual first.                                *
        * *******************************************************************************************************************************/
        #region Device Management

        //Get device's data storage status.For example,the count of administrators, count of users, etc
        //Please refer to our development manual to look over detailed parameters.
        private void btnGetDeviceStatus_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (cbStatus.Text.Trim() == "" || cbStatus.Text== "Status Type")
            {
                MessageBox.Show("Please choose the corresponding Status number ", "Error");
                return;
            }
            int idwErrorCode = 0;

            int idwValue=0;
            int idwStatus;//1-12,21,22
            string sdwStatus = cbStatus.Text.Trim();
            for (idwStatus = 22; idwStatus > 0; idwStatus--)////modify by Darcy on Nov.24 2009
            {
                if (sdwStatus.IndexOf(idwStatus.ToString()) > -1)
                {
                    break;
                }
            }

            if (axCZKEM1.GetDeviceStatus(iMachineNumber, idwStatus, ref idwValue))
            {
                txtGetDeviceStatus.Text = idwValue.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        //Get the device parameters
        private void btnGetDeviceInfo_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (cbInfo1.Text.Trim() == "" || cbInfo1.Text == "Info Type")
            {
                MessageBox.Show("Please choose the corresponding Info number ","Error");
                return;
            }
            int idwErrorCode = 0;

            int ivalue=0;
            int idwInfo;
            string sdwInfo = cbInfo1.Text.Trim();
            for (idwInfo = 68; idwInfo > 0; idwInfo--)
            {
                if (sdwInfo.IndexOf(idwInfo.ToString()) > -1)
                {
                    break;
                }
            }

            if (axCZKEM1.GetDeviceInfo(iMachineNumber,idwInfo,ref ivalue))
            {
                txtGetDeviceInfo.Text = ivalue.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        //Set the device parameters
        private void btnSetDeviceInfo_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (cbInfo2.Text.Trim() == "" || cbSetDeviceInfo.Text.Trim() == "" || cbInfo2.Text == "Info Type")
            {
                MessageBox.Show("Please choose the corresponding Info number and its value ", "Error");
                return;
            }
            int idwErrorCode = 0;

            int idwValue = 0;
            int idwInfo;
            string sdwInfo = cbInfo2.Text.Trim();
            for (idwInfo = 20; idwInfo > 0; idwInfo--)
            {
                if (sdwInfo.IndexOf(idwInfo.ToString()) > -1)
                {
                    break;
                }
            }
            idwValue =Convert.ToInt32(cbSetDeviceInfo.Text.Trim());
            if (axCZKEM1.SetDeviceInfo(iMachineNumber,idwInfo,idwValue))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully set the device information", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
        }

        //Synchronize the device time as the computer's.
        private void btnSetDeviceTime_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor=Cursors.WaitCursor;
            if (axCZKEM1.SetDeviceTime(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully set the time of the machine and the terminal to sync PC!", "Success");
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                if (axCZKEM1.GetDeviceTime(iMachineNumber, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond))//show the time
                {
                    txtGetDeviceTime.Text = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                }
             }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor=Cursors.Default;
        }

        //Custumize device's time as you want.
        private void btnSetDeviceTime2_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            int idwYear=Convert.ToInt32(cbYear.Text.Trim());
            int idwMonth=Convert.ToInt32(cbMonth.Text.Trim());
            int idwDay=Convert.ToInt32(cbDay.Text.Trim());
            int idwHour=Convert.ToInt32(cbHour.Text.Trim());
            int idwMinute=Convert.ToInt32(cbMinute.Text.Trim());
            int idwSecond=Convert.ToInt32(cbSecond.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SetDeviceTime2(iMachineNumber,idwYear,idwMonth,idwDay,idwHour,idwMinute,idwSecond))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully set the time of the machine as you have set!", "Error");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Obtain the device' current time
        private void btnGetDeviceTime_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            int idwYear=0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetDeviceTime(iMachineNumber,ref idwYear,ref idwMonth,ref idwDay,ref idwHour,ref idwMinute,ref idwSecond))
            {
                txtGetDeviceTime.Text = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the device's serial number
        private void btnGetSerialNumber_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwSerialNumber = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetSerialNumber(iMachineNumber,out sdwSerialNumber))
            {
                txtShow.Text = sdwSerialNumber;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the model of the device
        private void btnGetProductCode_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;
            
            string sProductCode = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetProductCode(iMachineNumber,out sProductCode))
            {
                txtShow.Text = sProductCode;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the Firmware version of the device
        private void btnGetFirmwareVersion_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sVersion= "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetFirmwareVersion(iMachineNumber,ref sVersion))
            {
                txtShow.Text = sVersion;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the version of the SDK you are using 
        private void btnGetSDKVersion_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sVersion = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetSDKVersion(ref sVersion))
            {
                txtShow.Text = sVersion;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the IP Address of the device
        private void btnGetDeviceIP_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sIP = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetDeviceIP(iMachineNumber,ref sIP))
            {
                txtShow.Text = sIP;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the MAC Address of the device
        private void btnGetDeviceMAC_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sMAC = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetDeviceMAC(iMachineNumber,ref sMAC))
            {
                txtShow.Text = sMAC;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Make sure whether the machine supports the RF card function.
        private void btnGetCardFun_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iCardFun=0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetCardFun(iMachineNumber,ref iCardFun))
            {
                txtShow.Text = iCardFun.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Query the device's current state
        //Please refer to our development manual for more detailed parameters information.
        private void btnQueryState_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iState = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.QueryState(ref iState))
            {
                txtShow.Text = iState.ToString();
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the name of the manufacturer
        private void btnGetVendor_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sVendor="";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetVendor(ref sVendor))
            {
                txtShow.Text = sVendor;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the platform of the device
        private void btnGetPlatform_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sPlatform = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetPlatform(iMachineNumber,ref sPlatform))
            {
                txtShow.Text = sPlatform;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Get the parameters of the device's options.
        private void btnGetSysOption_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sOption = "~PIN2Width";//You should input this parameter by yourself . 
            string sValue="";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetSysOption(iMachineNumber,sOption,out sValue))
            {
                txtShow.Text = sValue;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Play the specified number of continuous voice
        private void btnPlayVoice_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (cbPosition.Text.Trim() == "" || cbLength.Text.Trim()=="")
            {
                MessageBox.Show("Position and Length cannot be null!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iPosition = Convert.ToInt32(cbPosition.Text.Trim());
            int iLength = Convert.ToInt32(cbLength.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.PlayVoice(iPosition,iLength))
            {
                MessageBox.Show("Play Voice from "+iPosition.ToString()+" to "+iLength.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Play voice file according to its index
        private void btnPlayVoiceByIndex_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (cbIndex.Text.Trim() == "")
            {
                MessageBox.Show("Position(Voice Index) cannot be null!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iIndex = Convert.ToInt32(cbIndex.Text.Trim());
            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.PlayVoiceByIndex(iIndex))
            {
                MessageBox.Show("PlayVoiceByIndex " + iIndex.ToString() , "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Restart the device 
        private void btnRestartDevice_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.RestartDevice(iMachineNumber) == true)
            {
                MessageBox.Show("The device will restart!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;

        }

        //Power off the device
        private void btnPowerOffDevice_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.PowerOffDevice(iMachineNumber))
            {
                MessageBox.Show("PowerOffDevice", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }
        #endregion

        private void buttonGetDeviceFirmwareVersion_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.IsNewFirmwareMachine(iMachineNumber))
            {
                string strVersion = "";
                if (axCZKEM1.GetDeviceFirmwareVersion(iMachineNumber, ref strVersion))
                {
                    txtShow.Text = strVersion;
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                }
            }
            else
            {
                txtShow.Text = "This device is old firmware machine.";
            }
            Cursor = Cursors.Default;
        }

        private void buttonTurnOffAlarm_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            int idwErrorCode = 0;

            if (axCZKEM1.TurnOffAlarm(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Turn off Alarm success!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"Image file|*.jpg";
            ofd.ShowDialog();

            textBoxFileName.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "Please select download path";
            if (folder.ShowDialog() == DialogResult.OK)
            {
                textBoxDownloadPath.Text = folder.SelectedPath;
            }
        }

        private void buttonUploadTheme_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (textBoxFileName.Text.Trim() == "")
            {
                MessageBox.Show("Please select file first! ", "Error");
                return;
            }

            if (textBoxInDevName.Text.Trim() == "")
            {
                MessageBox.Show("Please input InDevName first! ", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            int idwErrorCode = 0;
            string strFileName = textBoxFileName.Text.Trim();
            string strInDevName = textBoxInDevName.Text.Trim();

            if (axCZKEM1.UploadTheme(iMachineNumber, strFileName, strInDevName))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Upload theme " + strFileName + "success!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonUploadPicture_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (textBoxFileName.Text.Trim() == "")
            {
                MessageBox.Show("Please select file first! ", "Error");
                return;
            }

            if (textBoxInDevName.Text.Trim() == "")
            {
                MessageBox.Show("Please input InDevName first! ", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            int idwErrorCode = 0;
            string strFileName = textBoxFileName.Text.Trim();
            string strInDevName = textBoxInDevName.Text.Trim();

            if (axCZKEM1.UploadPicture(iMachineNumber, strFileName, strInDevName))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Upload picture " + strFileName + "success!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonDownloadPicture_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (textBoxDownloadPath.Text.Trim() == "")
            {
                MessageBox.Show("Please select file first! ", "Error");
                return;
            }

            if (textBoxDownloadFile.Text.Trim() == "")
            {
                MessageBox.Show("Please input download file name first! ", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;
            int idwErrorCode = 0;
            string strFilePath = textBoxDownloadPath.Text.Trim() + "\\";
            string strFileName = textBoxDownloadFile.Text.Trim();

            if (axCZKEM1.DownloadPicture(iMachineNumber, strFileName, strFilePath))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Download picture " + strFileName + "success!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetAllSFIDName_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            int idwErrorCode = 0;

            string strShortkeyIDName = "";
            int iShortkeyIDName = 0;
            string strFunctionIDName = "";
            int iFunctionIDName = 0;

            comboBoxShortKeyID.Text = "";
            comboBoxFunctionID.Text = "";
            comboBoxShortKeyID.Items.Clear();
            comboBoxFunctionID.Items.Clear();

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetAllSFIDName(iMachineNumber, out strShortkeyIDName, iShortkeyIDName, out strFunctionIDName, iFunctionIDName))
            {
                while (true)
                {
                    strShortkeyIDName = strShortkeyIDName.Remove(0, strShortkeyIDName.IndexOf("\r\n"));
                    string[] separatingChars = { "\r\n" };
                    string[] tmpShortKey = strShortkeyIDName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in tmpShortKey)
                    {
                        string[] separatingChars2 = { "," };
                        string[] tmpShortKeyID = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                        comboBoxShortKeyID.Items.Add(tmpShortKeyID[0]);
                    }
                    strFunctionIDName = strFunctionIDName.Remove(0, strFunctionIDName.IndexOf("\r\n"));
                    string[] tmpFuncKey = strFunctionIDName.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in tmpFuncKey)
                    {
                        string[] separatingChars2 = { "," };
                        string[] tmpFuncKeyID = word.Split(separatingChars2, System.StringSplitOptions.RemoveEmptyEntries);
                        comboBoxFunctionID.Items.Add(tmpFuncKeyID[0]);
                    }
                    break;
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetShortkey_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (comboBoxShortKeyID.Text.Trim() == "")
            {
                MessageBox.Show("Please select ID first", "Error");
                return;
            }

            int idwErrorCode = 0;

            int iFunctionId = Convert.ToInt32(comboBoxShortKeyID.Text.Trim());
            string strShortKeyName = "";
            string strFunctionName = "";
            int iShortKeyFun = 0;
            int iStateCode = 0;
            string strStateName = "";
            string strDescription = "";
            int iAutoChange = 0;
            string strAutoChangeTime = "";

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetShortkey(iMachineNumber, iFunctionId, ref strShortKeyName, ref strFunctionName, ref iShortKeyFun, ref iStateCode, ref strStateName, ref strDescription, ref iAutoChange, ref strAutoChangeTime))
            {
                textBoxShortKeyName.Text = strShortKeyName;
                textBoxFunctionName.Text = strFunctionName;
                textBoxDescription.Text = strDescription;
                comboBoxShortKeyFun.Text = iShortKeyFun.ToString();
                if (iShortKeyFun == 1)
                {
                    comboBoxStateCode.Text = iStateCode.ToString();
                    textBoxStateName.Text = strStateName;
                    comboBoxAutoChange.Text = iAutoChange.ToString();
                    textBoxAutoChangeTime.Text = strAutoChangeTime;
                }
                else
                {
                    comboBoxStateCode.Text = "";
                    textBoxStateName.Text = "";
                    comboBoxAutoChange.Text = "";
                    textBoxAutoChangeTime.Text = "";
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonSetShortkey_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }

            if (comboBoxShortKeyID.Text.Trim() == "" || comboBoxShortKeyFun.Text.Trim() == "")
            {
                MessageBox.Show("Please input ShortKeyID and ShortKeyFun ! ", "Error");
                return;
            }

            if (comboBoxShortKeyFun.Text.Trim() == "1")
            {
                if (comboBoxStateCode.Text.Trim() == "" || textBoxStateName.Text.Trim() == "" || comboBoxAutoChange.Text.Trim() == "" || textBoxAutoChangeTime.Text.Trim() == "")
                {
                    MessageBox.Show("Please input the following parameters ");
                    return;
                }
            }

            int idwErrorCode = 0;

            int iShortKeyID = Convert.ToInt32(comboBoxShortKeyID.Text.Trim());
            int iFun = Convert.ToInt32(comboBoxShortKeyFun.Text.Trim());
            string strShortKeyName = textBoxShortKeyName.Text.Trim();
            string strFunctionName = textBoxFunctionName.Text.Trim();
            string strDescription = textBoxDescription.Text.Trim();

            Cursor = Cursors.WaitCursor;
            if (iFun == 1)//if the function key is status key.
            {
                int iStateCode = Convert.ToInt32(comboBoxStateCode.Text.Trim());
                string sStateName = textBoxStateName.Text.Trim();
                int iAutoChange = Convert.ToInt32(comboBoxAutoChange.Text.Trim());
                string sTime = textBoxAutoChangeTime.Text.Trim();
                if (axCZKEM1.SetShortkey(iMachineNumber, iShortKeyID, strShortKeyName, strFunctionName, iFun, iStateCode, sStateName, strDescription, iAutoChange, sTime))
                {
                    axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    MessageBox.Show("SetShortkey! ShortKeyID=" + iShortKeyID.ToString(), "Success");
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                }
            }
            else
            {
                int iStateCode = 0;
                string sStateName = "";
                int iAutoChange = 0;
                string sTime = "";

                if (axCZKEM1.SetShortkey(iMachineNumber, iShortKeyID, strShortKeyName, strFunctionName, iFun, iStateCode, sStateName, strDescription, iAutoChange, sTime))
                {
                    axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    MessageBox.Show("SetShortkey! ShortKeyID=" + iShortKeyID.ToString(), "Success");
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                }
            }

            Cursor = Cursors.Default;
        }

    }
} 