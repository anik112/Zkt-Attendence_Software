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
        private bool checkDataStoreOrNot = false;


        /**
         * Main working function in this program. this function pull data from machine buffer
         * and push into database.
         */
        public void consoleProcessForAttendence()
        {
            zkt = new CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class

            String tempFromDate=String.Empty;
            String tempToDate=String.Empty;

            while (true)
            {
                // get date from user
                Console.Write("From Date: ");
                tempFromDate = Console.ReadLine();
                // get date from user
                Console.Write("To Date: ");
                tempToDate = Console.ReadLine();
                // check given date is valid or not
                if(tempFromDate.Length==8 && tempToDate.Length == 8)
                {
                    int checkDayOfFromDate = Convert.ToInt32(tempFromDate.Substring(0, 2));
                    int checkMOnthOfFromDate = Convert.ToInt32(tempFromDate.Substring(2, 2));
                    int checkYearOfFromDate = Convert.ToInt32(tempFromDate.Substring(4, 4));
                    int checkDayOfToDate = Convert.ToInt32(tempToDate.Substring(0, 2));
                    int checkMonthOfToDate = Convert.ToInt32(tempToDate.Substring(2, 2));
                    int checkYearOfToDate = Convert.ToInt32(tempToDate.Substring(4, 4));

                    if ((checkDayOfFromDate>=1 && checkDayOfFromDate <= 31)
                        && (checkMOnthOfFromDate>=1 && checkMOnthOfFromDate<=12)
                        && (checkYearOfFromDate>=2000 && checkYearOfFromDate <= 3000)
                        && (checkDayOfToDate >= 1 && checkDayOfToDate <= 31)
                        && (checkMonthOfToDate >= 1 && checkMonthOfToDate <= 12)
                        && (checkYearOfToDate >= 2000 && checkYearOfToDate <= 3000))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n=> Sorry Date is not valid...\n");
                    }
                }
                else
                {
                    Console.WriteLine("\n=> Sorry Date is not valid...\n");
                }
            }
            // make final format of date
            workFromDate = tempFromDate.Substring(2, 2)+"/"+tempFromDate.Substring(0,2)+"/"+tempFromDate.Substring(4, 4);
            // make final format of date
            workToDate = tempToDate.Substring(2, 2) + "/" + tempToDate.Substring(0, 2) + "/" + tempToDate.Substring(4, 4);

            Console.WriteLine("\n------------------------- \n " + workFromDate + " to " + workToDate + "\n------------------------- \n");

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
                                checkDataStoreOrNot = true;
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
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("\n============================================\n" +
                                      "Device Number: " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress() +
                                      "\n*** Device is disconnected ***"+
                                      "\n============================================\n");
                }
                connection.Close(); //close database connection
            }

            if (checkDataStoreOrNot)
            {
                Console.WriteLine("\n\n*****************************************\n" +
                                 "           Data store in database            " +
                                 "\n*****************************************");
            }
            else
            {
                Console.WriteLine("\n\n*****************************************\n" +
                                    "       Data not store in database      " +
                                    "\n*****************************************");

            }
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
