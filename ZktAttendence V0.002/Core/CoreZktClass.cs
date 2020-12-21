using System;
using System.Collections.Generic;
using System.Text;
using zkemkeeper;
using ZktAttendence.Core_Service;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core
{
    class CoreZktClass : CoreZkt
    {
        public bool clearLogData(CZKEM objZkt, int machineNumber)
        {
            if (objZkt.ClearGLog(machineNumber))
            {
                return true;
            }
            return false;
        }

        // Attendence Data from Buffer
        public ICollection<AttendenceInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber)
        {
            try
            {
                string dwEnrollNumber = ""; // set EnrollNumber
                int dwVerifyMode = 0; // set Verify Mode
                int dwInOutMode = 0; // set in out mode
                int dwYear = 0; // set year
                int dwMonth = 0; // set month
                int dwDay = 0; // set day
                int dwHour = 0; // set hours
                int dwMinute = 0; // set minute
                int dwSecond = 0; // set second
                int dwWorkCode = 0; // set work code

                // make a arry list for take attendence log from buffer
                ICollection<AttendenceInfo> lstAttndData = new List<AttendenceInfo>();
                objZkt.ReadAllGLogData(machineNumber); // call ZKT libery function and set machineNumber

                // call ZKT libery function SSR_GetGeneralLogData(_) and fatch log data from buffer
                while (objZkt.SSR_GetGeneralLogData(machineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
                {
                    // make date from long time and date [ format like 05/29/2015 05:50:06 ]
                    string inputDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond).ToString("MM/dd/yyyy HH:mm:ss");
                    Console.WriteLine(">> " + inputDate);
                    // call MachineInfo call and access there propraty
                    AttendenceInfo objAttendenceInf = new AttendenceInfo();
                    objAttendenceInf.MachineNumber = machineNumber; // set machine number
                    objAttendenceInf.IndRegID = int.Parse(dwEnrollNumber); // set takeing attendence user id
                    objAttendenceInf.DateTimeRecord = inputDate; // set date of attendence

                    lstAttndData.Add(objAttendenceInf); // finaly add machineInfo object in array
                }

                return lstAttndData; // return array
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        

        // Device Connection
        public bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo)
        {
            try
            {
                /*
                 * Connect_Net()
                 * We get device connection using this method.**/
                bool check = cZKEM.Connect_Net(ipAddress, portNo); // Connection with Device
                if (check)
                {
                    return true; // return true
                }
                else
                {
                    return false; // return false
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo, int comPass)
        {
            try
            {
                /*
                 * Connect_Net()
                 * We get device connection using this method.**/
                if (cZKEM.SetCommPassword(comPass))
                {
                    if(cZKEM.Connect_Net(ipAddress, portNo))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }



        // Device Information
        public string GetDeviceInformation(CZKEM objZkeeper, int machineNumber)
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
            if (sWiegandFmt.Trim() != string.Empty)
            {
                sb.Append("Wiegand Fmt: ");
                sb.Append(sWiegandFmt);
                sb.Append(",");
            }

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

        public int GetMachineNumber(CZKEM objZkt)
        {
            return objZkt.MachineNumber;
        }



        // User ID List
        public ICollection<UserIdInfo> GetUserIdList(CZKEM objZkeeper, int machineNumber)
        {
            try
            {
                int dwEnrollNumber = 0; // set enroll number
                int dwEMachineNumber = 0; // set machine number
                int dwBackUpNumber = 0; // set backup number
                int dwMachinePrivelage = 0; // set machine privelage
                int dwEnabled = 0; // set machine is enable or not


                // create a array of userIdInfo object.
                ICollection<UserIdInfo> lstUserIDInfo = new List<UserIdInfo>();

                // call ZKT libery function GetAllUserID() for get user id list
                while (objZkeeper.GetAllUserID(machineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwBackUpNumber, ref dwMachinePrivelage, ref dwEnabled))
                {
                    // set data in UserIdInfo object
                    UserIdInfo userID = new UserIdInfo();
                    userID.BackUpNumber = dwBackUpNumber;
                    userID.Enabled = dwEnabled;
                    userID.EnrollNumber = dwEnrollNumber;
                    userID.MachineNumber = dwEMachineNumber;
                    userID.Privelage = dwMachinePrivelage;
                    lstUserIDInfo.Add(userID); // add the object in array
                }
                return lstUserIDInfo; // return array

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }



        // User Information
        public List<UserInfo> GetUserInformation(CZKEM objCzkem, int machineNumber)
        {
            try
            {
                // make a array for store UserInfo object
                List<UserInfo> listOfUser = new List<UserInfo>();

                string dwEnrollNumber = "";
                string Name = "";
                string Password = "";
                int Privilege = 0;
                bool Enabled = false;

                while (objCzkem.SSR_GetAllUserInfo(machineNumber, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled))
                {
                    UserInfo info = new UserInfo();
                    info.machineNumber = machineNumber;
                    info.dwEnrollNumber = int.Parse(dwEnrollNumber);
                    info.name = Name;
                    info.password = Password;
                    info.privilege = Privilege;
                    info.enable = Enabled;

                    listOfUser.Add(info);
                }
                return listOfUser; // return array

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

    }
}
