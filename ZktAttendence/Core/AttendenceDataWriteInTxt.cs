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
        private CoreZkt zkt;
        private CZKEM objZkt;
        private String workToDate = String.Empty;
        private String workFromDate = String.Empty;
        private bool checkDataStoreOrNot = false;
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

            Console.WriteLine("\n------------------------- \n " + workFromDate + " to " + workToDate + "\n------------------------- \n");

            ICollection<MachineSelector> getMachineList = new List<MachineSelector>();

            getMachineList = new SetupUtility().getDeviceSetupInformation(zktSetupPath, "deviceSetupInfo");
            FileStream file = new FileStream($"D:\\DATA\\{tempToDate}-{new Random().Next(10,99)}.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);

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
                                String[] part = chekingData.Split(' ');
                                String[] datePart = part[0].Split('/');
                                String finalDateWithFormat = datePart[2] + datePart[0] + datePart[1];
                                String[] timePart = part[1].Split(':');
                                String finalTimeWithFormat = timePart[0] + timePart[1] + timePart[2];

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
                                      "\n*** Device is disconnected ***" +
                                      "\n============================================\n");
                }
            }
            writer.Close();
            file.Close();
            return checkDataStoreOrNot;
        }

    }
}
