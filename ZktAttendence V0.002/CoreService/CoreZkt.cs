using System.Collections.Generic;
using zkemkeeper;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core_Service
{
    interface CoreZkt
    {
        // Get Connection with Device.
        bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo);
        // Get Connection with Device.
        bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo, int comPass);
        // Get Device infomation form device.
        string GetDeviceInformation(CZKEM objZkeeper, int machineNumber);
        // Get User Id List from Device.
        ICollection<UserIdInfo> GetUserIdList(CZKEM objZkeeper, int machineNumber);
        // Get User Information form Device.
        List<UserInfo> GetUserInformation(CZKEM objCzkem, int machineNumber);
        // Get Attendence Data from Buffer .
        ICollection<AttendenceInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber);
        // Clear log
        bool clearLogData(CZKEM objZkt, int machineNumber);
        // get machineNumber
        int GetMachineNumber(CZKEM objZkt);

    }
}
