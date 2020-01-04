using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;
using ZktAttendence.Core_Service;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core
{
    class CoreZktClass : CoreZkt
    {

        // Attendence Data from Buffer
        public ICollection<MachineInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber)
        {
            try
            {
                string dwEnrollNumber = "";
                int dwVerifyMode = 0;
                int dwInOutMode = 0;
                int dwYear = 0;
                int dwMonth = 0;
                int dwDay = 0;
                int dwHour = 0;
                int dwMinute = 0;
                int dwSecond = 0;
                int dwWorkCode = 0;

                ICollection<MachineInfo> lstAttndData = new List<MachineInfo>();

                objZkt.ReadAllGLogData(machineNumber);

                while (objZkt.SSR_GetGeneralLogData(machineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode))
                {
                    string inputDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond).ToString();

                    MachineInfo objMachineInfo = new MachineInfo();
                    objMachineInfo.MachineNumber = machineNumber;
                    objMachineInfo.IndRegID = int.Parse(dwEnrollNumber);
                    objMachineInfo.DateTimeRecord = inputDate;

                    lstAttndData.Add(objMachineInfo);
                }

                return lstAttndData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        
        // Device Connection
        public bool GetConnection(CZKEM cZKEM,string ipAddress, int portNo)
        {
            try
            {
                bool check=cZKEM.Connect_Net(ipAddress, portNo); // Connection with Device
                if (check)
                {
                    return true; // return true
                }
                else
                {
                    return false; // return false
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                //Response.Write(@"<SCRIPT LANGUAGE=""JavaScript"">alert('" + errorText + "')</SCRIPT>");
            }
            return false;
        }

        // Device Information
        public string GetDeviceInformation(CZKEM objZkeeper,int machineNumber)
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
            if(sWiegandFmt.Trim() != string.Empty)
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
                int dwEnrollNumber = 0;
                int dwEMachineNumber = 0;
                int dwBackUpNumber = 0;
                int dwMachinePrivelage = 0;
                int dwEnabled = 0;

                ICollection<UserIdInfo> lstUserIDInfo = new List<UserIdInfo>();

                while (objZkeeper.GetAllUserID(machineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwBackUpNumber, ref dwMachinePrivelage, ref dwEnabled))
                {
                    UserIdInfo userID = new UserIdInfo();
                    userID.BackUpNumber = dwBackUpNumber;
                    userID.Enabled = dwEnabled;
                    userID.EnrollNumber = dwEnrollNumber;
                    userID.MachineNumber = dwEMachineNumber;
                    userID.Privelage = dwMachinePrivelage;
                    lstUserIDInfo.Add(userID);
                }
                return lstUserIDInfo;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        // User Information
        public ICollection<UserInfo> GetUserInformation(CZKEM objCzkem, int machineNumber)
        {
            try
            {
                int dwMachineNumber = 0;
                int dwEnrollNumber = 0;
                string dwName = string.Empty;
                string dwPassword = string.Empty;
                int dwPrivilege = 0;
                bool dwEnable = false;

                ICollection<UserInfo> listOfUser = new List<UserInfo>();

                while(objCzkem.GetUserInfo(dwMachineNumber, dwEnrollNumber, ref dwName, ref dwPassword, ref dwPrivilege, ref dwEnable))
                {
                    UserInfo user = new UserInfo();
                    user.enrollNumber = dwEnrollNumber;
                    user.machineNumber = dwMachineNumber;
                    user.name = dwName;
                    user.password = dwPassword;
                    user.privilege = dwPrivilege;
                    user.enable = dwEnable;
                    listOfUser.Add(user);
                }
                
                return listOfUser;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
