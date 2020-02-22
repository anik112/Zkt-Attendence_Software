using System;
using System.Collections.Generic;
using ZktAttendence.Core_Service;
using ZktAttendence.Core;
using zkemkeeper;
using ZktAttendence.Utilitis;
using Oracle.DataAccess.Client;

namespace ZktAttendence
{
    class Master
    {
        private CoreZkt zkt;
        private CZKEM objZkt;

        public void consoleProcessForAttendence()
        {
            zkt = new CoreZktClass();
            objZkt = new CZKEM();
            int deviceCount = 0;
            int machineNumber = zkt.GetMachineNumber(objZkt);
            

            ICollection<MachineSelector> getMachineList = new UpdateInDatabase().getMachineListFromDatabase(DatabaseConnection.getConnection());

            foreach(MachineSelector selector in getMachineList)
            {
                Console.WriteLine("Device Number " + deviceCount + " IP: " + selector.getIpAddress());

                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                {
                    Console.WriteLine("*** Device is connected ***");
                    ICollection<MachineInfo> userAttndData = new List<MachineInfo>();
                    userAttndData = zkt.GetAttendenceLogData(objZkt, machineNumber);

                    int recordCount = 0;

                    foreach(MachineInfo machinAttendence in userAttndData)
                    {
/*                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("Machine Number: " + machinAttendence.MachineNumber);
                        Console.WriteLine("User Id: " + machinAttendence.IndRegID);
                        Console.WriteLine("Time & Date: " + machinAttendence.DateTimeRecord);*/
                        new UpdateInDatabase().storeLogDataInDatabase(machinAttendence.MachineNumber, 
                                                                     machinAttendence.getIndRegID(), 
                                                                     machinAttendence.DateTimeRecord,
                                                                     DatabaseConnection.getConnection()
                                                                     );
                        recordCount++;
                        Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"); // remove text from console
                        Console.Write("Process: " + recordCount + " Data"); // write text in console

                        if (recordCount > 50)
                        {
                            break;
                        }
                    }

                }
                else
                {
                    Console.WriteLine("\n Machine Number: " + selector.getMachineNumber() + " -- Is Disconnected --");
                }
                
            }

        }



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

                ICollection<MachineInfo> userAttndData = new List<MachineInfo>();
                userAttndData = zkt.GetAttendenceLogData(objZkt, machineNumber);

                cks = 0;

                OracleConnection oraConn = DatabaseConnection.getConnection();

                Console.WriteLine("-------------------- Attendence Data --------------------");
                foreach (MachineInfo m in userAttndData)
                {
                    cks++;
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Machine Number: " + m.MachineNumber);
                    Console.WriteLine("User Id: " + m.IndRegID);
                    Console.WriteLine("Time & Date: " + m.DateTimeRecord);
                    new UpdateInDatabase().storeLogDataInDatabase(m.MachineNumber,m.getIndRegID(),m.DateTimeRecord,oraConn);

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
