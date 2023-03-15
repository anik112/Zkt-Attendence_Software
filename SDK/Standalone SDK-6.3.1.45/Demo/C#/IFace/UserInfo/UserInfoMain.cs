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

namespace UserInfo
{
    public partial class UserInfoMain : Form
    {
        public UserInfoMain()
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

            axCZKEM1.PullMode = 1;
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

        /*************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
        * This part is for demonstrating operations with user(download/upload/delete/clear/modify).      *
        * ************************************************************************************************/
        #region UserInfo

        //Download user's 9.0 or 10.0 arithmetic fingerprint templates(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "GetUserTmpExStr" and "GetUserTmpEx".
        //'While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_GetUserTmp" or 
        //"SSR_GetUserTmpStr" instead of "GetUserTmpExStr" or "GetUserTmpEx" in order to download the fingerprint templates.
        private void btnDownloadTmp_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            lvDownload.Items.Clear();
            lvDownload.BeginUpdate();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            Cursor = Cursors.WaitCursor;

            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = sdwEnrollNumber;
                        list.SubItems.Add(sName);
                        list.SubItems.Add(idwFingerIndex.ToString());
                        list.SubItems.Add(sTmpData);
                        list.SubItems.Add(iPrivilege.ToString());
                        list.SubItems.Add(sPassword);
                        if (bEnabled == true)
                        {
                            list.SubItems.Add("true");
                        }
                        else
                        {
                            list.SubItems.Add("false");
                        }
                        list.SubItems.Add(iFlag.ToString());
                        lvDownload.Items.Add(list);
                    }
                }
            }
            lvDownload.EndUpdate();
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        //Upload the 9.0 or 10.0 fingerprint arithmetic templates to the device(in strings) in batches.
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "SetUserTmpExStr" and "SetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_SetUserTmp" or 
        //"SSR_SetUserTmpStr" instead of "SetUserTmpExStr" or "SetUserTmpEx" in order to upload the fingerprint templates.
        private void btnBatchUpdate_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (lvDownload.Items.Count == 0)
            {
                MessageBox.Show("There is no data to upload!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            bool bEnabled = false;
            int iFlag = 1;

            int iUpdateFlag = 1;

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.BeginBatchUpdate(iMachineNumber, iUpdateFlag))//create memory space for batching data
            {
                string sLastEnrollNumber = "";//the former enrollnumber you have upload(define original value as 0)
                for (int i = 0; i < lvDownload.Items.Count; i++)
                {
                    sdwEnrollNumber = lvDownload.Items[i].SubItems[0].Text;
                    sName = lvDownload.Items[i].SubItems[1].Text;
                    idwFingerIndex = Convert.ToInt32(lvDownload.Items[i].SubItems[2].Text);
                    sTmpData = lvDownload.Items[i].SubItems[3].Text;
                    iPrivilege = Convert.ToInt32(lvDownload.Items[i].SubItems[4].Text);
                    sPassword = lvDownload.Items[i].SubItems[5].Text;
                    sEnabled = lvDownload.Items[i].SubItems[6].Text;
                    iFlag = Convert.ToInt32(lvDownload.Items[i].SubItems[7].Text);

                    if (sEnabled == "true")
                    {
                        bEnabled = true;
                    }
                    else
                    {
                        bEnabled = false;
                    }
                    if (sdwEnrollNumber != sLastEnrollNumber)//identify whether the user information(except fingerprint templates) has been uploaded
                    {
                        if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                        {
                            axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the memory
                        }
                        else
                        {
                            axCZKEM1.GetLastError(ref idwErrorCode);
                            MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                            Cursor = Cursors.Default;
                            axCZKEM1.EnableDevice(iMachineNumber, true);
                            return;
                        }
                    }
                    else//the current fingerprint and the former one belongs the same user,that is ,one user has more than one template
                    {
                        axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
                    }
                    sLastEnrollNumber = sdwEnrollNumber;//change the value of iLastEnrollNumber dynamicly
                }
            }
            axCZKEM1.BatchUpdate(iMachineNumber);//upload all the information in the memory
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully upload fingerprint templates in batches , " + "total:" + lvDownload.Items.Count.ToString(), "Success");
        }

        //Upload the 9.0 or 10.0 fingerprint arithmetic templates one by one(in strings)
        //Only TFT screen devices with firmware version Ver 6.60 version later support function "SetUserTmpExStr" and "SetUserTmpEx".
        //While you are using 9.0 fingerprint arithmetic and your device's firmware version is under ver6.60,you should use the functions "SSR_SetUserTmp" or 
        //"SSR_SetUserTmpStr" instead of "SetUserTmpExStr" or "SetUserTmpEx" in order to upload the fingerprint templates.
        private void btnUploadTmp_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (lvDownload.Items.Count == 0)
            {
                MessageBox.Show("There is no data to upload!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            string sName = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iPrivilege = 0;
            string sPassword = "";
            int iFlag = 0;
            string sEnabled = "";
            bool bEnabled = false;

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvDownload.Items.Count; i++)
            {
                sdwEnrollNumber = lvDownload.Items[i].SubItems[0].Text.Trim();
                sName = lvDownload.Items[i].SubItems[1].Text.Trim();
                idwFingerIndex = Convert.ToInt32(lvDownload.Items[i].SubItems[2].Text.Trim());
                sTmpData = lvDownload.Items[i].SubItems[3].Text.Trim();
                iPrivilege = Convert.ToInt32(lvDownload.Items[i].SubItems[4].Text.Trim());
                sPassword = lvDownload.Items[i].SubItems[5].Text.Trim();

                sEnabled = lvDownload.Items[i].SubItems[6].Text.Trim();
                iFlag = Convert.ToInt32(lvDownload.Items[i].SubItems[7].Text);
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }

                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the device
                {
                    axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the device
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                    Cursor = Cursors.Default;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    return;
                }
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully Upload fingerprint templates, " + "total:" + lvDownload.Items.Count.ToString(), "Success");
        }

        //Delete a certain user's fingerprint template of specified index
        //You shuold input the the user id and the fingerprint index you will delete
        //The difference between the two functions "SSR_DelUserTmpExt" and "SSR_DelUserTmp" is that the former supports 24 bits' user id.
        private void btnSSR_DelUserTmpExt_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDTmp.Text.Trim() == "" || cbFingerIndex.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and FingerIndex first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDTmp.Text.Trim();
            int iFingerIndex = Convert.ToInt32(cbFingerIndex.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SSR_DelUserTmpExt(iMachineNumber, sUserID, iFingerIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("SSR_DelUserTmpExt,UserID:" + sUserID + " FingerIndex:" + iFingerIndex.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Clear all the fingerprint templates in the device(While the parameter DataFlag  of the Function "ClearData" is 2 )
        private void btnClearDataTmps_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iDataFlag = 2;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Clear all the fingerprint templates!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Delete all the user information in the device,while the related fingerprint templates will be deleted either. 
        //(While the parameter DataFlag  of the Function "ClearData" is 5 )
        private void btnClearDataUserInfo_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            int iDataFlag = 5;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearData(iMachineNumber, iDataFlag))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Clear all the UserInfo data!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Delete a kind of data that some user has enrolled
        //The range of the Backup Number is from 0 to 9 and the specific meaning of Backup number is described in the development manual,pls refer to it.
        private void btnDeleteEnrollData_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserIDDE.Text.Trim() == "" || cbBackupDE.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and BackupNumber first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserIDDE.Text.Trim();
            int iBackupNumber = Convert.ToInt32(cbBackupDE.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SSR_DeleteEnrollData(iMachineNumber, sUserID, iBackupNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("DeleteEnrollData,UserID=" + sUserID + " BackupNumber=" + iBackupNumber.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Clear all the administrator privilege(not clear the administrators themselves)
        private void btnClearAdministrators_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first", "Error");
                return;
            }
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.ClearAdministrators(iMachineNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("Successfully clear administrator privilege from teiminal!", "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        //Download users' face templates(in strings)(For TFT screen IFace series devices only)
        private void btnDownLoadFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            string sUserID = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int iFaceIndex = 50;//the only possible parameter value
            string sTmpData = "";
            int iLength = 0;

            lvFace.Items.Clear();
            lvFace.BeginUpdate();

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                {
                    ListViewItem list = new ListViewItem();
                    list.Text = sUserID;
                    list.SubItems.Add(sName);
                    list.SubItems.Add(sPassword);
                    list.SubItems.Add(iPrivilege.ToString());
                    list.SubItems.Add(iFaceIndex.ToString());
                    list.SubItems.Add(sTmpData);
                    list.SubItems.Add(iLength.ToString());
                    if (bEnabled == true)
                    {
                        list.SubItems.Add("true");
                    }
                    else
                    {
                        list.SubItems.Add("false");
                    }
                    lvFace.Items.Add(list);
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            lvFace.EndUpdate();
            Cursor = Cursors.Default;
        }

        //Upload users' face template(in strings)(For TFT screen IFace series devices only)
        //Uploading the face templates in batches is not supported temporarily.
        private void btnUploadFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = "";
            string sName = "";
            int iFaceIndex = 0;
            string sTmpData = "";
            int iLength = 0;
            int iPrivilege = 0;
            string sPassword = "";
            string sEnabled = "";
            bool bEnabled = false;

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < lvFace.Items.Count; i++)
            {
                sUserID = lvFace.Items[i].SubItems[0].Text;
                sName = lvFace.Items[i].SubItems[1].Text;
                sPassword = lvFace.Items[i].SubItems[2].Text;
                iPrivilege = Convert.ToInt32(lvFace.Items[i].SubItems[3].Text);
                iFaceIndex = Convert.ToInt32(lvFace.Items[i].SubItems[4].Text);
                sTmpData = lvFace.Items[i].SubItems[5].Text;
                if (sUserID == "3")
                {
                    sTmpData = "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BQYHSIkMAwIll0kLAsw9DQMZBQUKAQcMBAABBAIIBQAAAAABAAEOAQIEAAUCAAQAAwUEAAAABRkVBgkDBij/MQYFBQkjAAEBAAEBAAAAAAIBBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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BAkBBBkABA85BAITaycFBQgzIAcCBhUbEAoBAw0RLhMFAgpUoRQCAhydbAcEnDwMAiMHAgQCBQsBAQECAgQFAAAAAQIAAAACAAAAAAEBBQMHBwEBAAEFFhcDBgACKv8wBgAHIDIDAAMAAgAGAAABAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
                }
                iLength = Convert.ToInt32(lvFace.Items[i].SubItems[6].Text);
                if (sEnabled == "true")
                {
                    bEnabled = true;
                }
                else
                {
                    bEnabled = false;
                }
                bool result = false;
                if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sUserID, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
                {
                    result=axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, 50, sTmpData, iLength);//upload face templates information to the device
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                    Cursor = Cursors.Default;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                    return;
                }
            }

            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            MessageBox.Show("Successfully Upload the face templates, " + "total:" + lvFace.Items.Count.ToString(), "Success");
        }


        //Delete a certain user's face template according to its id
        private void btnDelUserFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserID3.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserID3.Text.Trim();
            int iFaceIndex = 50;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.DelUserFace(iMachineNumber, sUserID, iFaceIndex))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("DelUserFace,UserID=" + sUserID, "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;

        }

        //Download specified user's face template (in bytes array)    
        //You can refer to the part of "Udisk data Management" to learn how to manage the user's binary template(Get or Set)
        private void btnGetUserFace_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbUserID3.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbUserID3.Text.Trim();
            int iFaceIndex = 50;//the only possible parameter value
            int iLength = 128 * 1024;//initialize the length(cannot be zero)
            byte[] byTmpData = new byte[iLength];

            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);

            if (axCZKEM1.GetUserFace(iMachineNumber, sUserID, iFaceIndex, ref byTmpData[0], ref iLength))
            {
                //Here you can manage the information of the face templates according to your request.(for example,you can sava them to the database)
                MessageBox.Show("GetUserFace,the  length of the bytes array byTmpData is " + iLength.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }

            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        //add by Darcy on Nov.23 2009
        //Add the existed userid to DropDownLists.
        bool bAddControl = true;
        private void UserIDTimer_Tick(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                cbUserIDDE.Items.Clear();
                cbUserIDTmp.Items.Clear();
                cbVerifyUserID.Items.Clear();
                cbValidDateUserID.Items.Clear();
                bAddControl = true;
                return;
            }
            else
            {
                if (bAddControl == true)
                {
                    string sEnrollNumber = "";
                    string sName = "";
                    string sPassword = "";
                    int iPrivilege = 0;
                    bool bEnabled = false;

                    axCZKEM1.EnableDevice(iMachineNumber, false);
                    axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                    while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                    {
                        cbUserIDDE.Items.Add(sEnrollNumber);
                        cbUserIDTmp.Items.Add(sEnrollNumber);
                        cbVerifyUserID.Items.Add(sEnrollNumber);
                        cbValidDateUserID.Items.Add(sEnrollNumber);
                    }
                    bAddControl = false;
                    axCZKEM1.EnableDevice(iMachineNumber, true);
                }
                return;
            }
        }
        #endregion

        private void buttonGetUserVerifyStyle_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbVerifyUserID.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbVerifyUserID.Text.Trim();
            int iVerifyStyle = 0;
            byte bReserved  = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetUserVerifyStyle(iMachineNumber, sUserID, out iVerifyStyle, out bReserved))
            {
                string sStyle = "";
                switch (iVerifyStyle)
                {
                    case 0:
                        {
                            sStyle = "Group Verify";
                            break;
                        }
                    case 128:
                        {
                            sStyle = "FP/PW/RF";
                            break;
                        }
                    case 129:
                        {
                            sStyle = "FP";
                            break;
                        }
                    case 130:
                        {
                            sStyle = "PIN";
                            break;
                        }
                    case 131:
                        {
                            sStyle = "PW";
                            break;
                        }
                    case 132:
                        {
                            sStyle = "RF";
                            break;
                        }
                    case 133:
                        {
                            sStyle = "FP/PW";
                            break;
                        }
                    case 134:
                        {
                            sStyle = "FP/RF";
                            break;
                        }
                    case 135:
                        {
                            sStyle = "PW/RF";
                            break;
                        }
                    case 136:
                        {
                            sStyle = "PIN&FP";
                            break;
                        }
                    case 137:
                        {
                            sStyle = "FP&PW";
                            break;
                        }
                    case 138:
                        {
                            sStyle = "FP&RF";
                            break;
                        }
                    case 139:
                        {
                            sStyle = "PW&RF";
                            break;
                        }
                    case 140:
                        {
                            sStyle = "FP&PW&RF";
                            break;
                        }
                    case 141:
                        {
                            sStyle = "PIN&FP&PW";
                            break;
                        }
                    case 142:
                        {
                            sStyle = "FP&RF/PIN";
                            break;
                        }
                    default:
                        {
                            sStyle = "UNKNOWN";
                        }
                        break;
                }
                MessageBox.Show("Get User VerifyStyle,UserID=" + sUserID + " VerifyStyle=" + sStyle, "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonSetUserVerifyStyle_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbVerifyUserID.Text.Trim() == "" || cbVerifyStyle.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and VerifyStyle first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = cbVerifyUserID.Text.Trim();
            string sVerifyStyle = cbVerifyStyle.Text.Trim();
            int iVerifyStyle = 0;
            byte bReserved = 0;

            if (sVerifyStyle == "Group Verify")
            {
                iVerifyStyle = 0;
            }
            else if (sVerifyStyle == "FP/PW/RF")
            {
                iVerifyStyle = 128;
            }
            else if (sVerifyStyle == "FP")
            {
                iVerifyStyle = 129;
            }
            else if (sVerifyStyle == "PIN")
            {
                iVerifyStyle = 130;
            }
            else if (sVerifyStyle == "PW")
            {
                iVerifyStyle = 131;
            }
            else if (sVerifyStyle == "RF")
            {
                iVerifyStyle = 132;
            }
            else if (sVerifyStyle == "FP/PW")
            {
                iVerifyStyle = 133;
            }
            else if (sVerifyStyle == "FP/RF")
            {
                iVerifyStyle = 134;
            }
            else if (sVerifyStyle == "PW/RF")
            {
                iVerifyStyle = 135;
            }
            else if (sVerifyStyle == "PIN&FP")
            {
                iVerifyStyle = 136;
            }
            else if (sVerifyStyle == "FP&PW")
            {
                iVerifyStyle = 137;
            }
            else if (sVerifyStyle == "FP&RF")
            {
                iVerifyStyle = 138;
            }
            else if (sVerifyStyle == "PW&RF")
            {
                iVerifyStyle = 139;
            }
            else if (sVerifyStyle == "FP&PW&RF")
            {
                iVerifyStyle = 140;
            }
            else if (sVerifyStyle == "PIN&FP&PW")
            {
                iVerifyStyle = 141;
            }
            else if (sVerifyStyle == "FP&RF/PIN")
            {
                iVerifyStyle = 142;
            }
            else
            {
                iVerifyStyle = -1;
            }

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SetUserVerifyStyle(iMachineNumber, sUserID, iVerifyStyle, ref bReserved))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("Set User VerifyStyle,UserID=" + sUserID + " VerifyStyle=" + sVerifyStyle, "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonGetUserValidDate_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbValidDateUserID.Text.Trim() == "")
            {
                MessageBox.Show("Please select user ID first!", "Error");
                return;
            }

            string strUserId = cbValidDateUserID.Text.Trim();
            int iExpires = 0;
            int iValidCount = 0;
            string strStartDate = "";
            string strEndDate = "";
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.GetUserValidDate(iMachineNumber, strUserId, out iExpires, out iValidCount, out strStartDate, out strEndDate))
            {
                comboBoxExpires.Text = iExpires.ToString();
                textBoxValidCount.Text = iValidCount.ToString();
                textBoxStartTime.Text = strStartDate;
                textBoxEndTime.Text = strEndDate;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void buttonSetUserValidDate_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (cbValidDateUserID.Text.Trim() == "")
            {
                MessageBox.Show("Please select user ID first!", "Error");
                return;
            }

            if (comboBoxExpires.Text.Trim() == "" || textBoxValidCount.Text.Trim() == ""
                || textBoxStartTime.Text.Trim() == "" || textBoxEndTime.Text.Trim() == "")
            {
                MessageBox.Show("Please input other information!", "Error");
                return;
            }

            string strUserId = cbValidDateUserID.Text.Trim();
            int iExpires = Convert.ToInt32(comboBoxExpires.Text.Trim());
            int iValidCount = Convert.ToInt32(textBoxValidCount.Text.Trim());
            string strStartDate = textBoxStartTime.Text.Trim();
            string strEndDate = textBoxEndTime.Text.Trim();
            int idwErrorCode = 0;

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SetUserValidDate(iMachineNumber, strUserId, iExpires, iValidCount, strStartDate, strEndDate))
            {
                axCZKEM1.RefreshData(iMachineNumber);
                MessageBox.Show("Set user valid date success!", "Error");
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