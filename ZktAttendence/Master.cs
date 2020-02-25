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
        private String workToDate=String.Empty;
        private String workFromDate = String.Empty;


        /**
         * Main working function in this program. this function pull data from machine buffer
         * and push into database.
         */
        public void consoleProcessForAttendence()
        {
            zkt = new CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class
            
            // get date from user
            Console.Write("From Date: ");
            String tempFromDate = Console.ReadLine();
            // get date from user
            Console.Write("To Date: ");
            String tempToDate = Console.ReadLine();
            // make final format of date
            workFromDate = tempFromDate.Substring(2, 2)+"/"+tempFromDate.Substring(0,2)+"/"+tempFromDate.Substring(4, 4);
            // make final format of date
            workToDate = tempToDate.Substring(2, 2) + "/" + tempToDate.Substring(0, 2) + "/" + tempToDate.Substring(4, 4);

            Console.WriteLine("\n ------------------------- \n " + workFromDate + " to " + workToDate + "\n ------------------------- \n");

            // take machine information from database
            ICollection <MachineSelector> getMachineList = new UpdateInDatabase().getMachineListFromDatabase(DatabaseConnection.getConnection());

            // patch machine information
            foreach(MachineSelector selector in getMachineList)
            {

                Console.WriteLine("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

                OracleConnection connection = DatabaseConnection.getConnectionWithoutMsg();
                // get device connection using UTP protocol
                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                {
                    Console.WriteLine("*** Device is connected ***\n");
                    // if device connected then make a object array
                    ICollection<MachineInfo> userAttndData = new List<MachineInfo>();
                    // get attendence data from device buffer
                    userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());

                    int recordCount = 0;// record counter
                    // patch attendence data
                    foreach(MachineInfo machinAttendence in userAttndData)
                    {
                        
                        String chekingData = machinAttendence.DateTimeRecord;
                        if(chekingData.Contains(workFromDate) || chekingData.Contains(workToDate))
                        {
                            // check the data exists or not in database, which data come from device buffer
                            if (new UpdateInDatabase().checkIfIsNotExists(machinAttendence.DateTimeRecord, connection))
                            {
                                // Console.WriteLine("[ " + machinAttendence.getIndRegID() + " & " + chekingData+" ]");
                                // if not exists in database then store this data
                                new UpdateInDatabase().storeLogDataInDatabase(machinAttendence.MachineNumber,
                                                                             machinAttendence.getIndRegID(),
                                                                             chekingData,
                                                                             connection
                                                                             );
                                recordCount++; // record counter
                                Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"); // remove text from console
                                Console.Write("Process: " + recordCount + " Data"); // write text in console

/*                                if (recordCount > 50)
                                {
                                    break;
                                }*/
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n============================================\n" +
                                      "Device Number: " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress() +
                                      "\n*** Device is disconnected ***"+
                                      "\n============================================\n");
                }
                connection.Close();
            }

            Console.WriteLine("\n\n*****************************************\n" +
                                  "            Data store complete            " +
                                  "\n*****************************************");  
        }





        /**
         * this is a test perpose function for checking
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
