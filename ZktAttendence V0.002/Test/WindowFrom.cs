using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using zkemkeeper;
using ZktAttendence.Core_Service;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Test
{
    public partial class WindowFrom : Form
    {
        private CoreZkt zkt; // make zkt libs object
        private CZKEM objZkt;  // make zkt libs object
        private String formatString = "105:00020001990:20191125:195420:11";
        private String zktFilePath = String.Empty; // Declare for file location
        private String workToDate = String.Empty; // declare to date variable
        private String workFromDate = String.Empty; // decalre from date variable
        private bool isClear = false;

        public WindowFrom(String setupFilePath)
        {
            InitializeComponent();
            this.zktFilePath = setupFilePath;
            txtFromDate.Select();
            txtDateForClear.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtDateOfRTA.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void dgnBtnProcess_Click(object sender, EventArgs e)
        {        

            if (txtFromDate.Text != String.Empty && txtToDate.Text != String.Empty)
            {
                processStart();
            }
            else
            {
                setMsgInBox("\nPlease set the date properly and try again.");
            }
            
        }


        private void txtToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtToDate.Text.Length == 8)
            {
                String workToDate = txtToDate.Text.Substring(2, 2) + "/" + txtToDate.Text.Substring(0, 2) + "/" + txtToDate.Text.Substring(4, 4);
                txtToDate.Text = workToDate;
                dgnBtnProcess.Focus();
            }
        }
        
        private void txtFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtFromDate.Text.Substring(2, 2) + "/" + txtFromDate.Text.Substring(0, 2) + "/" + txtFromDate.Text.Substring(4, 4);
                txtFromDate.Text = workFromDate;
                txtToDate.Focus();

            }
        }

        private void txtFromDate_MouseClick(object sender, MouseEventArgs e)
        {
            txtFromDate.Text = String.Empty;
        }

        private void txtToDate_MouseClick(object sender, MouseEventArgs e)
        {
            txtToDate.Text = String.Empty;
        }

        private void txtFromDateForClear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtDateForClear.Text.Substring(2, 2) + "/" + txtDateForClear.Text.Substring(0, 2) + "/" + txtDateForClear.Text.Substring(4, 4);
                txtDateForClear.Text = workFromDate;

            }
        }

        private void txtFromDateForClear_MouseClick(object sender, MouseEventArgs e)
        {
            txtDateForClear.Text = String.Empty;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CZKEM objZktV2 = new CZKEM(); // create object of Lib class

            if (checkBoxSelected.Checked == true)
            {
                isClear = true;
                processStart();

                try
                {
                    List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info
                    getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array

                    foreach (MachineSelector selector in getMachineList)
                    {
                        setMsgInBox("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

                        if (zkt.GetConnection(objZktV2, selector.getIpAddress(), selector.getPortNumber()))
                        {
                            if (zkt.clearLogData(objZktV2, selector.getMachineNumber()))
                            {
                                MessageBox.Show("Device Log Clear Successfull");
                            }
                        }
                        else
                        {
                            setMsgInBox("\n*** Can't Clear Log Data ***\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    setMsgInBox("\n\n" + ex.Message + "\n\n");
                }
            } 
            
        }
        private void setMsgInBox(String msg)
        {
            txtShowMsg.Text += msg;
            txtShowMsg.Invalidate();
            txtShowMsg.Update();

            txtShowMsg.Focus();
            txtShowMsg.SelectionStart = txtShowMsg.Text.Length;
        }


        private void processStart()
        {
            zkt = new Core.CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class
            String[] tempDateParts;
            String tempToDate;

            workFromDate = txtFromDate.Text;
            workToDate = txtToDate.Text;
            

            if (isClear)
            {
                tempDateParts = new string[1];
                tempToDate = String.Empty;
                setMsgInBox("\n>> -- Starting Backup -- <<\n");
            }
            else
            {
                tempDateParts = workToDate.Split('/');
                tempToDate = tempDateParts[1] + tempDateParts[0] + tempDateParts[2];
                setMsgInBox("\n>> -- " + workFromDate + " TO " + workToDate + " -- <<\n");
            }

            /**
            * From this part i get all device from stored file and store in a array.
            * then i traversing this array and get one by one device info.
            * then i execute 'GetAttendenceLogData(objZkt, selector.getMachineNumber())' this function for
            * get attendence.
             */
            List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info

            getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array
            FileStream file;
            if (isClear)
            {
                file = new FileStream($"D:\\DATA\\{DateTime.Now.ToString("MMddyyyhhmm")}-CL.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            }
            else
            {
                 file = new FileStream($"D:\\DATA\\{tempToDate}{DateTime.Now.ToString("hhmm")}.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            }
            StreamWriter writer = new StreamWriter(file); // open file by file writer
            List<UserInfo> userList = new List<UserInfo>(); // store all user in list
                                                            // patch machine information
            foreach (MachineSelector selector in getMachineList)
            {
                setMsgInBox("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

                // get device connection using UTP protocol
                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                {
                    setMsgInBox("\n*** Device is connected ***\n");

                    ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();// if device connected then make a object array
                                                                                           // get attendence data from device buffer
                    userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());
                    userList.AddRange(zkt.GetUserInformation(objZkt, selector.getMachineNumber()));

                    int recordCount = 0;// record counter

                    try
                    {
                        
                        if (isClear)
                        {
                            // patch attendence data
                            foreach (AttendenceInfo machinAttendence in userAttndData)
                            {
                                String chekingData = machinAttendence.DateTimeRecord;
                                //105:00020001990:20191125:195420:11
                                String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20'
                                String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                String finalDateWithFormat = datePart[2] + datePart[0] + datePart[1];
                                String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];
                                
                                foreach (UserInfo user in userList)
                                {
                                    if (user.dwEnrollNumber == machinAttendence.IndRegID)
                                    {
                                        // Write file in selected file location
                                        writer.WriteLine($"{machinAttendence.MachineNumber}:{user.name}:{finalDateWithFormat}:{finalTimeWithFormat}:11");
                                        //Console.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID);
                                    }
                                }
                            }
                            setMsgInBox("\nData Backup Successful\n");
                        }
                        else
                        {
                            // patch attendence data
                            foreach (AttendenceInfo machinAttendence in userAttndData)
                            {
                                String chekingData = machinAttendence.DateTimeRecord;

                                if (chekingData.Contains(workFromDate) || chekingData.Contains(workToDate))
                                {
                                    //105:00020001990:20191125:195420:11
                                    String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20'
                                    String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                    String finalDateWithFormat = datePart[2] + datePart[0] + datePart[1];
                                    String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                    String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];

                                    foreach (UserInfo user in userList)
                                    {
                                        if (user.dwEnrollNumber == machinAttendence.IndRegID)
                                        {
                                            // Write file in selected file location
                                            writer.WriteLine($"{machinAttendence.MachineNumber}:{user.name}:{finalDateWithFormat}:{finalTimeWithFormat}:11");
                                            //Console.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID);
                                        }
                                    }

                                    recordCount++;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            setMsgInBox("\nProcess: " + recordCount + " Data\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        setMsgInBox("\n\n" + ex.Message + "\n\n");
                    }
                }
                else
                {
                    setMsgInBox("\n*** Device is disconnected ***\n");
                }
            }

            writer.Close();
            file.Close();

            txtShowMsg.Invalidate();
            txtShowMsg.Update();

            txtShowMsg.Focus();
            txtShowMsg.SelectionStart = txtShowMsg.Text.Length;

            if (!isClear)
            {
                MessageBox.Show("Please Check In D:\\DATA\\" + tempToDate + DateTime.Now.ToString("hhmm"));
            }

            txtShowMsg.Text += ("\n>> -- Done -- <<\n");
        }

        private void btnProcessRTA_Click(object sender, EventArgs e)
        {
            setMsgInBox("\n>> -- Starting Download From RTA -- <<\n");
            Process process = Process.Start("D:\\RTA600E SDK\\rta600v10.exe");
            process.WaitForExit();
            setMsgInBox("\n>> -- Downloaded -- <<\n");
        }
    }
}
