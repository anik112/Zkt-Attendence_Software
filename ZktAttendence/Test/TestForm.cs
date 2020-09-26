using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using zkemkeeper;
using ZktAttendence.Core_Service;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Test
{
    public partial class TestForm : Form
    {
        private CoreZkt zkt; // make zkt libs object
        private CZKEM objZkt;  // make zkt libs object
        private String formatString = "105:00020001990:20191125:195420:11";
        private String zktFilePath = String.Empty; // Declare for file location
        private String workToDate = String.Empty; // declare to date variable
        private String workFromDate = String.Empty; // decalre from date variable

        public TestForm(String setupFilePath)
        {
            InitializeComponent();
            this.zktFilePath = setupFilePath;
            txtFromDate.Select();
            txtDateForClear.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }

        private void dgnBtnProcess_Click(object sender, EventArgs e)
        {
            zkt = new Core.CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class
            String[] tempDateParts;
            String tempToDate;

            if (txtFromDate.Text != String.Empty && txtToDate.Text != String.Empty)
            {
                workFromDate = txtFromDate.Text;
                workToDate = txtToDate.Text;
                tempDateParts = workToDate.Split('/');
                tempToDate = tempDateParts[1] + tempDateParts[0] + tempDateParts[2];

                txtShowMsg.Text += "\n>> -- "+ workFromDate+" TO "+workToDate+" -- <<\n";
                txtShowMsg.Invalidate();
                txtShowMsg.Update();

                /**
                * From this part i get all device from stored file and store in a array.
                * then i traversing this array and get one by one device info.
                * then i execute 'GetAttendenceLogData(objZkt, selector.getMachineNumber())' this function for
                * get attendence.
                 */
                List <MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info

                getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array
                FileStream file = new FileStream($"D:\\DATA\\{tempToDate}{DateTime.Now.ToString("hhmm")}.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
                StreamWriter writer = new StreamWriter(file); // open file by file writer
                List<UserInfo> userList = new List<UserInfo>(); // store all user in list
                                                                // patch machine information
                foreach (MachineSelector selector in getMachineList)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    txtShowMsg.Text+=("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());
                    //txtShowMsg.Invalidate();
                    txtShowMsg.Update();

                    // get device connection using UTP protocol
                    if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        txtShowMsg.Text+=("\n*** Device is connected ***\n");
                        txtShowMsg.Invalidate();
                        txtShowMsg.Update();

                        ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();// if device connected then make a object array
                                                                                               // get attendence data from device buffer
                        userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());
                        userList.AddRange(zkt.GetUserInformation(objZkt, selector.getMachineNumber()));

                        int recordCount = 0;// record counter
                        try
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
                            txtShowMsg.Text+=("\nProcess: " + recordCount + " Data\n"); // write text in console
                            txtShowMsg.Invalidate();
                            txtShowMsg.Update();
                        }
                        catch (Exception ex)
                        {
                            txtShowMsg.Text += ("\n\n" + ex.Message+"\n\n");
                            txtShowMsg.Invalidate();
                            txtShowMsg.Update();
                        }
                    }
                    else
                    {
                        txtShowMsg.Text += ("\n*** Device is disconnected ***\n");
                        txtShowMsg.Invalidate();
                        txtShowMsg.Update();
                    }
                }

                writer.Close();
                file.Close();

                txtShowMsg.Text += ("\n>> -- Done -- <<\n");
                txtShowMsg.Invalidate();
                txtShowMsg.Update();

                MessageBox.Show("Please Check In D:\\DATA\\"+tempToDate+DateTime.Now.ToString("hhmm"));
            }
            else
            {
                txtShowMsg.Text += ("\nPlease set the date properly and try again.");
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
            try
            {
                List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info
                getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array

                foreach (MachineSelector selector in getMachineList)
                {
                    txtShowMsg.Text += ("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());
                    txtShowMsg.Invalidate();
                    txtShowMsg.Update();

                    if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                    {
                        if (zkt.clearLogData(objZkt, selector.getMachineNumber()))
                        {
                            txtShowMsg.Text += "\n*** Device Log Clear Successfull ***\n";
                            txtShowMsg.Invalidate();
                            txtShowMsg.Update();
                        }
                    }
                    else
                    {
                        txtShowMsg.Text += "\n*** Can't Clear Log Data ***\n";
                        txtShowMsg.Invalidate();
                        txtShowMsg.Update();
                    }
                }
            }
            catch(Exception ex)
            {
                txtShowMsg.Text += ("\n\n" + ex.Message + "\n\n");
                txtShowMsg.Invalidate();
                txtShowMsg.Update();
            }
               
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
