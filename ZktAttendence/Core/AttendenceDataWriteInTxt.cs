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
        private String workToDate = String.Empty; // declare to date variable
        private String workFromDate = String.Empty; // decalre from date variable
        private bool checkDataStoreOrNot = false; // checking variable
        private String formatString = "105:00020001990:20191125:195420:BLANK !!:11";


        /**
         * Main working function in this program. this function pull data from machine buffer
         * and push into database.
         */
        public bool consoleProcessForAttendence(String zktSetupPath)
        {
            zkt = new CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class

            String tempFromDate = String.Empty;
            String tempToDate = String.Empty;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                // get date from user
                Console.Write("From Date: ");
                tempFromDate = Console.ReadLine();
                // get date from user
                Console.Write("To Date: ");
                tempToDate = Console.ReadLine();
                // check given date is valid or not
                if (tempFromDate.Length == 8 && tempToDate.Length == 8)
                {
                    // Formating given data into Day, Month, Year
                    int checkDayOfFromDate = Convert.ToInt32(tempFromDate.Substring(0, 2));
                    int checkMOnthOfFromDate = Convert.ToInt32(tempFromDate.Substring(2, 2));
                    int checkYearOfFromDate = Convert.ToInt32(tempFromDate.Substring(4, 4));
                    int checkDayOfToDate = Convert.ToInt32(tempToDate.Substring(0, 2));
                    int checkMonthOfToDate = Convert.ToInt32(tempToDate.Substring(2, 2));
                    int checkYearOfToDate = Convert.ToInt32(tempToDate.Substring(4, 4));

                    if ((checkDayOfFromDate >= 1 && checkDayOfFromDate <= 31)
                        && (checkMOnthOfFromDate >= 1 && checkMOnthOfFromDate <= 12)
                        && (checkYearOfFromDate >= 2000 && checkYearOfFromDate <= 3000)
                        && (checkDayOfToDate >= 1 && checkDayOfToDate <= 31)
                        && (checkMonthOfToDate >= 1 && checkMonthOfToDate <= 12)
                        && (checkYearOfToDate >= 2000 && checkYearOfToDate <= 3000))
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n=> Sorry Date is not valid...\n");
                    }
                }
                // If data isn't valid
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n=> Sorry Date is not valid...\n");
                }
            }
            // make final format of date
            workFromDate = tempFromDate.Substring(2, 2) + "/" + tempFromDate.Substring(0, 2) + "/" + tempFromDate.Substring(4, 4);
            // make final format of date
            workToDate = tempToDate.Substring(2, 2) + "/" + tempToDate.Substring(0, 2) + "/" + tempToDate.Substring(4, 4);
            // Show massage
            Console.WriteLine("\n------------------------- \n " + workFromDate + " to " + workToDate + "\n------------------------- \n");

            /**
             * From this part i get all device from stored file and store in a array.
             * then i traversing this array and get one by one device info.
             * then i execute 'GetAttendenceLogData(objZkt, selector.getMachineNumber())' this function for
             * get attendence.
             */
            ICollection<MachineSelector> getMachineList = new List<MachineSelector>(); // call the array for store device info

            getMachineList = new SetupUtility().getDeviceSetupInformation(zktSetupPath, "deviceSetupInfo"); // get all device info in array
            FileStream file = new FileStream($"D:\\DATA\\{tempToDate}-{new Random().Next(10,99)}.txt", FileMode.Create, FileAccess.Write); // Make file path for store data
            StreamWriter writer = new StreamWriter(file); // open file by file writer

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
                    // if device connected then make a object array
                    ICollection<AttendenceInfo> userAttndData = new List<AttendenceInfo>();
                    // get attendence data from device buffer
                    userAttndData = zkt.GetAttendenceLogData(objZkt, selector.getMachineNumber());

                    int recordCount = 0;// record counter
                    try
                    {
                        // patch attendence data
                        foreach (AttendenceInfo machinAttendence in userAttndData)
                        {
                            String chekingData = machinAttendence.DateTimeRecord;
                            if (chekingData.Contains(workFromDate) || chekingData.Contains(workToDate))
                            {
                                //105:00020001990:20191125:195420:BLANK!!:11
                                String[] part = chekingData.Split(' '); // string like '19:54:20 2020/08/20'
                                String[] datePart = part[0].Split('/'); // '2020/08/20' to {2020,08,20}
                                String finalDateWithFormat = datePart[2] + datePart[0] + datePart[1];
                                String[] timePart = part[1].Split(':'); // '19:54:20' to {19,54,20}
                                String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];
                                // Write file in selected file location
                                writer.WriteLine($"{machinAttendence.MachineNumber}:{machinAttendence.empName}:{finalDateWithFormat}:{finalTimeWithFormat}:BLANK!!:11");

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
                                      "Device Number: " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress() +
                                      "\n  *** Device is disconnected ***" +
                                      "\n============================================\n");
                }
            }
            writer.Close();
            file.Close();
            return checkDataStoreOrNot;
        }

    }
}
