using System.Collections.Generic;
using zkemkeeper;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core_Service
{
    interface CoreZkt
    {

        // Get Connection with Device.
        bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo);
        // Get Device infomation form device.
        string GetDeviceInformation(CZKEM objZkeeper, int machineNumber);
        // Get User Id List from Device.
        ICollection<UserIdInfo> GetUserIdList(CZKEM objZkeeper, int machineNumber);
        // Get User Information form Device.
        ICollection<UserInfo> GetUserInformation(CZKEM objCzkem, int machineNumber);
        // Get Attendence Data from Buffer .
        ICollection<AttendenceInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber);
        // get machineNumber
        int GetMachineNumber(CZKEM objZkt);

    }
}
