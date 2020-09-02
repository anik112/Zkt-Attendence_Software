using System;
using zkemkeeper;
using System.Text;
using System.Collections.Generic;
using HelloWorld.dlls;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO.Ports;

namespace HelloWorld
{
    class Program
    {

        static void Main()
        {
            Console.SetWindowSize(1, 1);
            while (true)
            {

            }
            /*Process process = Process.Start("D:\\RTA600E SDK\\rta600v10.exe");
            process.WaitForExit();
            Console.WriteLine("Execute OK... !");*/
            //SerialPort port = new SerialPort("COM2", 9600, Parity.Even, 8, StopBits.One);
            /*try
            {
                SerialPort port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
                port.Open();
                Console.WriteLine("Port Open.");
                char[] result = new char[1];
                for (int len = 0; len < result.Length;)
                {
                    len += port.Read(result, len, result.Length - len);
                    Console.WriteLine(len);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Port can't open: " + e.Message);
            }
            Console.ReadLine();*/

        }


        public void runApps()
        {
            /*
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Hello World! - am anik & My ID is :"+id);

            if (id > 20)
            {
                Console.WriteLine("You are Old!! :)");
            }
            else
            {
                Console.WriteLine("You are yong ::");
            }

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }

            int[] itemListCode=new int[5];
            itemListCode[0] = 120;
            itemListCode[1] = 201;
            itemListCode[2] = 521;
            for(int i=0;i<itemListCode.Length; i++)
            {
                Console.WriteLine("=> "+itemListCode[i]);
            }
            TestDat user = new TestDat(100);
            Console.WriteLine(user.getId());
            */

            string ipAddress = string.Empty;
            int portNo = 0;

            Console.WriteLine("[y] = Continue Program, [n] = Exit Program");

            while (Console.ReadLine() != "n")
            {

                Console.Write("Ip Address: ");
                ipAddress = Console.ReadLine();
                Console.Write("Port No: ");
                portNo = Convert.ToInt32(Console.ReadLine());

                zkemkeeper.CZKEM cZKEM = new CZKEM();


                // -------------------------------------- Get conntection ------------------
                // port no is 4370
                bool check = false;
                try
                {
                    check = cZKEM.Connect_Net(ipAddress, portNo);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                int outDwMachineNumber = cZKEM.MachineNumber;
                Console.WriteLine("Machine NUmber: " + outDwMachineNumber);

                int machineNumber = 0;

                if (check)
                {
                    Console.WriteLine("Device is Connected.");


                    bool beepCheck = cZKEM.Beep(1000);
                    Console.WriteLine(beepCheck);
                    if (cZKEM.PowerOffDevice(outDwMachineNumber))
                    {
                        Console.WriteLine("Device is OFF\n");
                    }
                    else
                    {
                        Console.WriteLine("Device is ON\n");
                    }

                    //---------------------------------------- Get Device info -------------------------------------------------------

                    StringBuilder sb = new StringBuilder();
                    string returnValue = string.Empty;
                    string finalText;


                    try
                    {
                        cZKEM.GetFirmwareVersion(machineNumber, ref returnValue);
                        if (returnValue.Trim() != string.Empty)
                        {
                            sb.Append("Firmware V: ");
                            sb.Append(returnValue);
                            sb.Append(",");
                            finalText = "Virson: " + returnValue;
                        }

                        Console.WriteLine(sb);
                        Console.WriteLine(returnValue);


                        sb.Clear();
                        returnValue = string.Empty;
                        cZKEM.GetSerialNumber(outDwMachineNumber, out returnValue);
                        if (returnValue.Trim() != string.Empty)
                        {
                            sb.Append("Serial No: ");
                            sb.Append(returnValue);
                            sb.Append(",");
                        }
                        else
                        {
                            Console.WriteLine("No Serial Number.");
                        }
                        Console.WriteLine(sb);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //---------------------------- Get Use Id dtls -------------------------------------

                    Program p = new Program();
                    /*
                    Console.WriteLine("=> USER ID ----------------------- <=");
                    ICollection<UserIdinfo> lstUserIDInfo = new List<UserIdinfo>();
                    lstUserIDInfo = p.GetAllUserID(cZKEM, outDwMachineNumber);
                    Console.WriteLine("User count: " + lstUserIDInfo.Count);
                    foreach (UserIdinfo u in lstUserIDInfo)
                    {
                        Console.WriteLine("================");
                        Console.WriteLine("Machine Number: " + u.MachineNumber);
                        Console.WriteLine("Backup Number: " + u.BackUpNumber);
                        Console.WriteLine("Enroll NUmber: " + u.EnrollNumber);
                        Console.WriteLine("Enable: " + u.Enabled);
                        Console.WriteLine("Privilege: " + u.Privelage);
                        Console.WriteLine("================");
                    }*/

                    //--------------------------- Get user info dtls -------------------------------------

                    Console.WriteLine("\n=> USER info ----------------------- <=");
                    ICollection<UserInfo> listOfUser = new List<UserInfo>();
                    listOfUser = p.GetUserInfo(cZKEM, outDwMachineNumber);

                    foreach (UserInfo u in listOfUser)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Machine Number: " + u.machineNumber);
                        Console.WriteLine("Enroll NUmber: " + u.enrollNumber);
                        Console.WriteLine("Name: " + u.name);
                        Console.WriteLine("Password: " + u.password);
                        Console.WriteLine("Privilege: " + u.privilege);
                        Console.WriteLine("Enable: " + u.enable);
                    }
                    listOfUser.Clear();

                    Console.WriteLine("\n------------------ Device Info ----------------------");

                    string s = p.FetchDeviceInfo(cZKEM, outDwMachineNumber);
                    Console.WriteLine(s);


                    //------------------------------ Get attendence data log -----------------------------------------------

                    ICollection<MachineInfo> lstEnrollData = new List<MachineInfo>();

                    lstEnrollData = p.GetLogData(cZKEM, outDwMachineNumber);

                    Console.WriteLine("\n============== Log Data ================");
                    foreach (MachineInfo m in lstEnrollData)
                    {
                        Console.WriteLine("________________________");
                        Console.WriteLine("User Id: " + m.IndRegID);
                        Console.WriteLine("Date & Time: " + m.DateTimeRecord);
                        Console.WriteLine("________________________");
                    }
                    lstEnrollData.Clear();

                }
                else
                {
                    Console.WriteLine("Not Connected.");
                }

                ipAddress = string.Empty;
                portNo = 0;
                cZKEM.Disconnect();
                Console.WriteLine("[y] = Continue Program, [n] = Exit Program");
            }
        }

        /*
         * GETALLUSERID(CZKEM X,INT Z);
         * This function help's us to get device information from device;
         */
        public ICollection<UserIdinfo> GetAllUserID(CZKEM objZkeeper, int machineNumber)
        {
            int dwEnrollNumber = 0;
            int dwEMachineNumber = 0;
            int dwBackUpNumber = 0;
            int dwMachinePrivelage = 0;
            int dwEnabled = 0;

            ICollection<UserIdinfo> lstUserIDInfo = new List<UserIdinfo>();

            while (objZkeeper.GetAllUserID(machineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwBackUpNumber, ref dwMachinePrivelage, ref dwEnabled))
            {
                UserIdinfo userID = new UserIdinfo();
                userID.BackUpNumber = dwBackUpNumber;
                userID.Enabled = dwEnabled;
                userID.EnrollNumber = dwEnrollNumber;
                userID.MachineNumber = dwEMachineNumber;
                userID.Privelage = dwMachinePrivelage;
                lstUserIDInfo.Add(userID);
            }
            return lstUserIDInfo;
        }


        public ICollection<UserInfo> GetUserInfo(CZKEM objCzkem,int machineNumber)
        {
            int dwMachineNumber=0;
            int dwEnrollNumber=0;
            string dwName=string.Empty;
            string dwPassword=string.Empty;
            int dwPrivilege=0;
            bool dwEnable=false;

            ICollection<UserInfo> listOfUser = new List<UserInfo>();

            objCzkem.GetUserInfo(dwMachineNumber, dwEnrollNumber, ref dwName, ref dwPassword, ref dwPrivilege, ref dwEnable);
                UserInfo user = new UserInfo();
                user.enrollNumber = dwEnrollNumber;
                user.machineNumber = dwMachineNumber;
                user.name = dwName;
                user.password = dwPassword;
                user.privilege = dwPrivilege;
                user.enable = dwEnable;
                listOfUser.Add(user);
            

            return listOfUser;
        }


        

        public string FetchDeviceInfo(CZKEM objZkeeper, int machineNumber)
        {
            StringBuilder sb = new StringBuilder();

            string returnValue = string.Empty;


            objZkeeper.GetFirmwareVersion(machineNumber, ref returnValue);
            if (returnValue.Trim() != string.Empty)
            {
                sb.Append("Firmware V: ");
                sb.Append(returnValue);
                sb.Append(",");
            }


            returnValue = string.Empty;
            objZkeeper.GetVendor(ref returnValue);
            if (returnValue.Trim() != string.Empty)
            {
                sb.Append("Vendor: ");
                sb.Append(returnValue);
                sb.Append(",");
            }

            string sWiegandFmt = string.Empty;
            objZkeeper.GetWiegandFmt(machineNumber, ref sWiegandFmt);

            returnValue = string.Empty;
            objZkeeper.GetSDKVersion(ref returnValue);
            if (returnValue.Trim() != string.Empty)
            {
                sb.Append("SDK V: ");
                sb.Append(returnValue);
                sb.Append(",");
            }

            returnValue = string.Empty;
            objZkeeper.GetSerialNumber(machineNumber, out returnValue);
            if (returnValue.Trim() != string.Empty)
            {
                sb.Append("Serial No: ");
                sb.Append(returnValue);
                sb.Append(",");
            }

            returnValue = string.Empty;
            objZkeeper.GetDeviceMAC(machineNumber, ref returnValue);
            if (returnValue.Trim() != string.Empty)
            {
                sb.Append("Device MAC: ");
                sb.Append(returnValue);
            }

            return sb.ToString();
        }


        /// <summary>
        ///  Get attendence data form ZKT buffer.
        ///  we used ReadAllGLogData() method from zkt .dll
        /// </summary>
        /// <param name="objZkeeper">objZkeeper: This perametter get a boject.</param>
        /// <param name="machineNumber">machineNumber: This perametter get machin number.</param>
        /// <returns>
        /// It return a list of log data. A array of MachineInfo class
        /// </returns>
        public ICollection<MachineInfo> GetLogData(CZKEM objZkeeper, int machineNumber)
        {
            string dwEnrollNumber1 = "";
            int dwVerifyMode = 0;
            int dwInOutMode = 0;
            int dwYear = 0;
            int dwMonth = 0;
            int dwDay = 0;
            int dwHour = 0;
            int dwMinute = 0;
            int dwSecond = 0;
            int dwWorkCode = 0;

            ICollection<MachineInfo> lstEnrollData = new List<MachineInfo>();

            objZkeeper.ReadAllGLogData(machineNumber);

            while (objZkeeper.SSR_GetGeneralLogData(machineNumber, out dwEnrollNumber1, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
            {
                string inputDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond).ToString();

                MachineInfo objInfo = new MachineInfo();
                objInfo.MachineNumber = machineNumber;
                objInfo.IndRegID = int.Parse(dwEnrollNumber1);
                objInfo.DateTimeRecord = inputDate;

                lstEnrollData.Add(objInfo);
            }

            return lstEnrollData;
        }

    }

}
