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
        private String workToDay=String.Empty;
        private String workFromDay = String.Empty;


        public void consoleProcessForAttendence()
        {
            zkt = new CoreZktClass();
            objZkt = new CZKEM();
            int machineNumber = zkt.GetMachineNumber(objZkt);

            Console.WriteLine("From Date: ");
            String tempFromDate = Console.ReadLine();
            Console.WriteLine("To Date: ");
            String tempToDate = Console.ReadLine();

            workFromDay = tempFromDate.Substring(3, 4)+"/"+tempFromDate.Substring(1,2)+"/"+tempFromDate.Substring(4,8);
            workToDay = tempToDate.Substring(3, 4) + "/" + tempToDate.Substring(1, 2) + "/" + tempToDate.Substring(4, 8);


            ICollection<MachineSelector> getMachineList = new UpdateInDatabase().getMachineListFromDatabase(DatabaseConnection.getConnection());

            foreach(MachineSelector selector in getMachineList)
            {

                Console.WriteLine("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                {
                    Console.WriteLine("*** Device is connected ***\n");
                    ICollection<MachineInfo> userAttndData = new List<MachineInfo>();
                    userAttndData = zkt.GetAttendenceLogData(objZkt, machineNumber);

                    int recordCount = 0;

                    foreach(MachineInfo machinAttendence in userAttndData)
                    {
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
