using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info
        private List<MachineSelector> selectedMachineList = new List<MachineSelector>(); // call the array for store device info
        private CheckBox[] box;

        public WindowFrom(String setupFilePath)
        {
            InitializeComponent();
            this.zktFilePath = setupFilePath; // set ZKT path
            viewMachineList(); // view machine list in panel
            txtFromDate.Select(); // set date
            txtDateForClear.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDateOfRTA.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void dgnBtnProcess_Click(object sender, EventArgs e)
        {
            /*
             * Check From date is not empty
             * Check To date is not empty
             * Chekc from date month is not upper then to date month
             */

            if ((txtFromDate.Text != String.Empty)
                && (txtToDate.Text != String.Empty)
                && (int.Parse(txtFromDate.Text.Substring(3, 2)) <= int.Parse(txtToDate.Text.Substring(3, 2)))
            )
            {
                Console.WriteLine("In-String Format");
                Console.WriteLine("From Date: "+txtFromDate.Text.Substring(0, 2));
                setSelectedMachineList();
                int errorStatus = processStart();
                if (errorStatus == 1001)
                {
                    setMsgInBox("\nPlease set the date between 30 days.");
                }
                else if (errorStatus == 1021)
                {
                    setMsgInBox("\nInternal error occurred when data registering in file.");
                }
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
                String workToDate = txtToDate.Text.Substring(0, 2) + "/" + txtToDate.Text.Substring(2, 2) + "/" + txtToDate.Text.Substring(4, 4);
                txtToDate.Text = workToDate;
                dgnBtnProcess.Focus();
            }
        }

        private void txtFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtFromDate.Text.Substring(0, 2) + "/" + txtFromDate.Text.Substring(2, 2) + "/" + txtFromDate.Text.Substring(4, 4);
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
            if (txtFromDate.Text.Length == 8)
            {
                String workFromDate = txtDateForClear.Text.Substring(0, 2) + "/" + txtDateForClear.Text.Substring(2, 2) + "/" + txtDateForClear.Text.Substring(4, 4);
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
                setSelectedMachineList();

                isClear = true;
                // if any error arries 
                int errorStatus = processStart();
                if (errorStatus == 1001)
                {
                    setMsgInBox("\nPlease set the date between 30 days.");
                }
                else if (errorStatus == 1021)
                {
                    setMsgInBox("\nInternal error occurred when data registering in file.");
                }

                try
                {
                    List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info
                    getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array

                    foreach (MachineSelector selector in selectedMachineList)
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


        private int processStart()
        {
            setMsgInBox("\n>> -- Into Process Function -- <<\n");
            zkt = new Core.CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class
            String[] tempDateParts;
            String tempToDate;

            workFromDate = txtFromDate.Text;
            workToDate = txtToDate.Text;

            // get list of date which is in from - to date. Date range
            /* Last update 22-11-2020, 02-12-2020 */
            string[] workDates = new string[32];
            int index = 0;
            workDates[0] = (txtFromDate.Text);
            DateTime fromdt = DateTime.Parse(txtFromDate.Text);
            Console.WriteLine("=>" + fromdt);

            DateTime toDate = DateTime.Parse(txtToDate.Text);

            Console.WriteLine("=>" + toDate);

            
            while (!workDates[index].Equals(txtToDate.Text))
            {
                fromdt = fromdt.AddDays(1);
                workDates[index + 1] = fromdt.Date.ToString("dd/MM/yyyy"); //fromdt.Date.ToString("MM/dd/yyyy");
                index++;
                if (index > 31)
                {
                    return 1001;
                }
                Console.WriteLine("=>" + fromdt);
                setMsgInBox("\n>> -- Make Index -- <<\n");
            }
            

            if (isClear)
            {
                tempDateParts = new string[1];
                tempToDate = String.Empty;
                setMsgInBox("\n>> -- Starting Backup -- <<\n");
            }
            else
            {
                tempDateParts = workToDate.Split('/');
                tempToDate = tempDateParts[0] + tempDateParts[1] + tempDateParts[2];
                setMsgInBox("\n>> -- " + workFromDate + " TO " + workToDate + " -- <<\n");
            }
            /**
            * From this part i get all device from stored file and store in a array.
            * then i traversing this array and get one by one device info.
            * then i execute 'GetAttendenceLogData(objZkt, selector.getMachineNumber())' this function for
            * get attendence.
             */
            FileStream file;
            if (isClear)
            {
                file = new FileStream($"D:\\DATA\\{DateTime.Now.ToString("ddMMyyyyhhmm")}-CL.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            }
            else
            {
                file = new FileStream($"D:\\DATA\\{tempToDate}{DateTime.Now.ToString("hhmm")}.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            }
            StreamWriter writer = new StreamWriter(file); // open file by file writer
            List<UserInfo> userList = new List<UserInfo>(); // store all user in list
                                                            //  patch machine information
                                                            //   Last update - 17-11-2020

            foreach (MachineSelector selector in selectedMachineList)
            {
                setMsgInBox("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress() + " - " + selector.getAddress());
                Console.WriteLine("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress() + " - " + selector.getAddress());

                // get device connection using UTP protocol
                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber(), selector.getcomPass()))
                {
                    setMsgInBox("\n*** Device is connected ***\n");
                    Console.WriteLine("\n*** Device is connected ***\n");

                    ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();// if device connected then make a object array
                                                                                           // get attendence data from device buffer
                    userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());
                    //userList.AddRange(zkt.GetUserInformation(objZkt, selector.getMachineNumber())); // Last update - 17-11-2020

                    int recordCount = 0;// record counter
                    try
                    {

                        if (isClear)
                        {
                            // patch attendence data
                            foreach (AttendenceInfo machinAttendence in userAttndData)
                            {
                                String chekingData = machinAttendence.DateTimeRecord;

                                //Console.WriteLine(chekingData);

                                //105:00020001990:20191125:195420:11
                                String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20'
                                String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                String finalDateWithFormat = datePart[2] + datePart[1] + datePart[0];
                                String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];

                                writer.WriteLine($"{machinAttendence.MachineNumber}:{machinAttendence.IndRegID.ToString().PadLeft(10, '0')}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // chnage in 16-11-2020

                                // last update - 17-11-2020
                                /*foreach (UserInfo user in userList)
                                {
                                    if (user.dwEnrollNumber.ToString() == machinAttendence.IndRegID)
                                    {
                                        // Write file in selected file location
                                        //writer.WriteLine($"{machinAttendence.MachineNumber}:{user.name}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // Change in 10-10-2020
                                        writer.WriteLine($"{machinAttendence.MachineNumber}:{machinAttendence.IndRegID.ToString().PadLeft(10, '0')}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // chnage in 16-11-2020
                                        //Console.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID);
                                    }
                                }*/
                            }
                            setMsgInBox("\nData Backup Successful\n");
                        }
                        else
                        {
                            // patch attendence data
                            foreach (AttendenceInfo machinAttendence in userAttndData)
                            {
                                String chekingData = machinAttendence.DateTimeRecord;

                                Console.WriteLine("Not Clear From Reg. Data: "+chekingData);


                                bool isFalse = false;
                                for (int i = 0; (i < workDates.Length) && (workDates[i] != null); i++)
                                {
                                    if (chekingData.Contains(workDates[i]))
                                    {
                                       
                                        //105:00020001990:20191125:195420:11
                                        String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20' // 22/05/2021 07:10:19
                                        String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                        //setMsgInBox($"\n{chekingData}\n");
                                        String finalDateWithFormat = datePart[2] + datePart[1] + datePart[0];
                                        String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                        String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];

                                        writer.WriteLine($"{machinAttendence.MachineNumber}:{machinAttendence.IndRegID.ToString().PadLeft(10, '0')}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // chnage in 16-11-2020
                                        // setMsgInBox($"{machinAttendence.MachineNumber}:{machinAttendence.IndRegID.ToString().PadLeft(10, '0')}:{finalDateWithFormat}:{finalTimeWithFormat}:11");
                                        // last update - 17-11-2020
                                        /*foreach (UserInfo user in userList)
                                        {
                                            if (user.dwEnrollNumber.ToString() == machinAttendence.IndRegID)
                                            {
                                                // Write file in selected file location
                                                //writer.WriteLine($"{machinAttendence.MachineNumber}:{user.name}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // Change in 10-10-2020
                                                writer.WriteLine($"{machinAttendence.MachineNumber}:{machinAttendence.IndRegID.ToString().PadLeft(10, '0')}:{finalDateWithFormat}:{finalTimeWithFormat}:11"); // chnage in 16-11-2020                                                                                                                                                         //writer.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID.ToString().PadLeft(10,'0') +" >> "+machinAttendence.DateOnlyRecord);
                                                Console.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID);
                                            }
                                        }*/

                                        recordCount++;
                                    }
                                    else
                                    {
                                        isFalse = true;
                                    }
                                }

                                if (isFalse)
                                {
                                    continue;
                                }
                            }
                            setMsgInBox("\nProcess: " + recordCount + " Data\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        setMsgInBox(ex.Message);
                        Console.WriteLine("Into-Process: "+ex.Message);
                        setMsgInBox(ex.ToString());
                        setMsgInBox(GetLineNumber(ex).ToString());
                        return 1021;
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
            return 0;
        }

        private void btnProcessRTA_Click(object sender, EventArgs e)
        {
            setMsgInBox("\n>> -- Starting Download From RTA -- <<\n\n");
            Process process = Process.Start("RTA600.exe");
            process.WaitForExit();

            /*
            string logPath = System.IO.Path.Combine(Environment.CurrentDirectory, "log.txt");
            StreamReader reader = new StreamReader(logPath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if(line.Contains("Open COM Port Success"))
                {
                    setMsgInBox("\n");
                }
                setMsgInBox(line + "\n");
            }
            reader.Close();
            */
            setMsgInBox("\n>> -- Downloaded -- <<\n");
        }

        // find the range of date
        private List<String> findDateRange(String fromDate, String toDate)
        {
            List<String> dateList = new List<String>();
            dateList.Add(fromDate);
            dateList.Add(toDate);

            DateTime fromdt = DateTime.Parse(fromDate);
            DateTime todt = DateTime.Parse(toDate);

            for (DateTime dt = fromdt; dt < todt; dt.AddDays(1))
            {
                Console.WriteLine(dt.ToString("dd/MM/yyyy"));
                dateList.Add(dt.ToString("dd/MM/yyyy"));
            }

            return dateList;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (isDeselectAll.Checked)
            {
                isSelectAll.Checked = false;

                for (int i = 0; i < box.Length; i++)
                {
                    box[i].Checked = false;
                }
            }
        }

        private void setSelectedMachineList()
        {
            selectedMachineList.Clear();

            for (int i = 0; i < box.Length; i++)
            {
                if (box[i].Checked && (!isDeselectAll.Checked))
                {
                    foreach (MachineSelector s in getMachineList)
                    {
                        if (s.getIpAddress() == box[i].Name)
                        {
                            selectedMachineList.Add(s);
                        }
                    }
                }
            }

            if (selectedMachineList.Count == 0)
            {
                selectedMachineList = getMachineList;
                viewMachineList();
            }


            foreach (MachineSelector r in selectedMachineList)
            {
                Console.WriteLine(">> " + r.getIpAddress());
            }
            Console.WriteLine(selectedMachineList.Count);
        }

        private void viewMachineList()
        {
            tablePan.Controls.Clear();
            getMachineList = new SetupUtility().getDeviceSetupInformation(zktFilePath, "deviceSetupInfo"); // get all device info in array
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


        private void tablePan_MouseEnter(object sender, EventArgs e)
        {
            isDeselectAll.Checked = false;
        }

        private void WindowFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            string logPath = System.IO.Path.Combine(Environment.CurrentDirectory, "log.txt");
            File.Delete(logPath);
        }

        private void isSelectAll_Click(object sender, EventArgs e)
        {
            if (isSelectAll.Checked)
            {
                isDeselectAll.Checked = false;

                for (int i = 0; i < box.Length; i++)
                {
                    box[i].Checked = true;
                }
            }
        }

        private void menuFileAddDevice_Click(object sender, EventArgs e)
        {
            AddDevice addDevice = new AddDevice(zktFilePath);
            addDevice.Show();
        }

        private int GetLineNumber(Exception ex)
        {

            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            int ln = 0;
            if (index != -1)
            {


                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                string lnum = System.Text.RegularExpressions.Regex.Match(lineNumberText, @"\d+").Value;
                int.TryParse(lnum, out ln);

            }
            return ln;
        }

    }
}