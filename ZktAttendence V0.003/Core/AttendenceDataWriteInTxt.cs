using System;
using System.Collections.Generic;
using ZktAttendence.Core_Service;
using zkemkeeper;
using ZktAttendence.Utilitis;
using System.IO;


namespace ZktAttendence.Core
{
    public class AttendenceDataWriteInTxt
    {
        private CoreZkt zkt; // make zkt libs object
        private CZKEM objZkt;  // make zkt libs object
        private bool checkDataStoreOrNot = false; // checking variable
        private String formatString = "105:00020001990:20191125:195420:11";


        /**
         * Main working function in this program. this function pull data from machine buffer
         * and push into database.
         */
        public bool consoleProcessForAttendence(String zktSetupPath, String workFromDate, String workToDate)
        {
            zkt = new CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class
            String[] tempDateParts;
            String tempToDate;

            if (workToDate != null)
            {
                tempDateParts = workToDate.Split('/');
                tempToDate = tempDateParts[1] + tempDateParts[0] + tempDateParts[2];
            }
            else
            {
                return false;
            }

            

            /**
             * From this part i get all device from stored file and store in a array.
             * then i traversing this array and get one by one device info.
             * then i execute 'GetAttendenceLogData(objZkt, selector.getMachineNumber())' this function for
             * get attendence.
             */
            List<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info

            getMachineList = new SetupUtility().getDeviceSetupInformation(zktSetupPath, "deviceSetupInfo"); // get all device info in array
            FileStream file = new FileStream($"D:\\DATA\\{tempToDate}{DateTime.Now.ToString("hhmm")}.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            StreamWriter writer = new StreamWriter(file); // open file by file writer
            List<UserInfo> userList = new List<UserInfo>(); // store all user in list
            // patch machine information
            foreach (MachineSelector selector in getMachineList)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

                // get device connection using UTP protocol
                if (zkt.GetConnection(objZkt, selector.getIpAddress(), selector.getPortNumber()))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** Device is connected ***\n");
                    ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();// if device connected then make a object array
                    // get attendence data from device buffer
                    userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());
                    userList.AddRange(zkt.GetUserInformation(objZkt, selector.getMachineNumber()));

                    int recordCount = 0;// record counter
                    try
                    {
                        // patch attendence data
                        foreach (AttendenceInfo machinAttendence in userAttndData)
                        {
                            String chekingData = machinAttendence.DateTimeRecord;
                            Console.WriteLine(machinAttendence.MachineNumber + " -> " + chekingData);
                            if (chekingData.Contains(workFromDate) || chekingData.Contains(workToDate))
                            {
                                //105:00020001990:20191125:195420:11
                                String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20'
                                String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                String finalDateWithFormat = datePart[2] + datePart[0] + datePart[1];
                                String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];

                                foreach (UserInfo user in userList)
                                {
                                    if (user.dwEnrollNumber.ToString() == machinAttendence.IndRegID )
                                    {
                                        // Write file in selected file location
                                        writer.WriteLine($"{machinAttendence.MachineNumber}:{user.name}:{finalDateWithFormat}:{finalTimeWithFormat}:11");
                                        //Console.WriteLine(">>>>> " + machinAttendence.MachineNumber + " >>" + user.name + " >>" + machinAttendence.IndRegID);
                                    }
                                }

                                recordCount++;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"); // remove text from console
                                Console.Write("Process: " + recordCount + " Data"); // write text in console
                            }
                            else
                            {
                                continue;
                            }
                        }
                        checkDataStoreOrNot = true;
                        Console.WriteLine("\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n" + e.Message);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n============================================\n" +
                                      "        *** Device is disconnected ***" +
                                      "\n============================================\n");
                }
            }

            writer.Close();
            file.Close();
            return checkDataStoreOrNot;
        }

    }
}
