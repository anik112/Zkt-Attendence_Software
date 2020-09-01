using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using zkemkeeper;
using ZktAttendence.Core;
using ZktAttendence.Core_Service;
using ZktAttendence.Utilitis;

namespace ZktAttendence
{
    class Master
    {

        private CoreZkt zkt;
        private CZKEM objZkt;

        /**
         * this is a test perpose function for checking
         * Old function
         */
        public void DriverMethod()
        {
            zkt = new CoreZktClass();
            objZkt = new CZKEM();

            Console.WriteLine("[y] = Continue Program, [n] = Exit Program");

            while (Console.ReadLine() != "n")
            {
                Console.Write("IP Add: ");
                string ipAdd = Console.ReadLine();
                //int port = 4370;
                Console.Write("Port Number: ");
                int port = int.Parse(Console.ReadLine());

                if (zkt.GetConnection(objZkt, ipAdd, port))
                {
                    Console.WriteLine("Device Connected. !");
                }
                else
                {
                    Console.WriteLine("Device NOt Connected. !");
                    continue;
                }

                int machineNumber = zkt.GetMachineNumber(objZkt);

                string deviceInfo = zkt.GetDeviceInformation(objZkt, machineNumber);

                Console.WriteLine(deviceInfo);

                ICollection<UserIdInfo> userList = new List<UserIdInfo>();
                userList = zkt.GetUserIdList(objZkt, machineNumber);

                int cks = 0;

                Console.WriteLine("------------------- User Data ---------------------");
                foreach (UserIdInfo u in userList)
                {
                    cks++;
                    Console.WriteLine("Machine Number: " + u.MachineNumber);
                    Console.WriteLine("Enroll Number: " + u.EnrollNumber);
                    Console.WriteLine("Backup Number: " + u.BackUpNumber);
                    Console.WriteLine("");

                    if (cks > 50)
                    {
                        break;
                    }
                }
                userList.Clear();

                ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();
                userAttndData = zkt.GetAttendenceLogData(objZkt, machineNumber);

                cks = 0;

                OracleConnection oraConn = DatabaseConnection.getConnection();

                Console.WriteLine("-------------------- Attendence Data --------------------");
                foreach (AttendenceInfo m in userAttndData)
                {
                    cks++;
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Machine Number: " + m.MachineNumber);
                    Console.WriteLine("User Id: " + m.IndRegID);
                    Console.WriteLine("Time & Date: " + m.DateTimeRecord);
                    new UpdateInDatabase().storeLogDataInDatabase(m.MachineNumber, m.getIndRegID(), m.DateTimeRecord, oraConn);

                    if (cks > 50)
                    {
                        break;
                    }
                }
                userAttndData.Clear();

                Console.WriteLine("\n[y] = Continue Program, [n] = Exit Program");
            }

        }
    }
}
