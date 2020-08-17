using System;
using System.Collections.Generic;
using ZktAttendence.Core_Service;
using zkemkeeper;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core
{
    class AttendenceDataWriteInTxt
    {
        private CoreZkt zkt;
        private CZKEM objZkt;
        private String workToDate = String.Empty;
        private String workFromDate = String.Empty;
        private bool checkDataStoreOrNot = false;


        /**
         * Main working function in this program. this function pull data from machine buffer
         * and push into database.
         */
        public void consoleProcessForAttendence(String zktSetupPath)
        {
            zkt = new CoreZktClass(); // create object of Core class
            objZkt = new CZKEM(); // create object of Lib class

            String tempFromDate = String.Empty;
            String tempToDate = String.Empty;

            while (true)
            {
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
                        Console.WriteLine("\n=> Sorry Date is not valid...\n");
                    }
                }
                else
                {
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

            // patch machine information
            foreach (MachineSelector selector in getMachineList)
            {

                Console.WriteLine("\nDevice Number " + selector.getMachineNumber() + " - IP: " + selector.getIpAddress());

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
                    foreach (MachineInfo machinAttendence in userAttndData)
                    {

                        String chekingData = machinAttendence.DateTimeRecord;
                        if (chekingData.Contains(workFromDate) || chekingData.Contains(workToDate))
                        {
                            
/*
                            
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

                                *//*                                if (recordCount > 50)
                                                                {
                                                                    break;
                                                                }*//*
                                checkDataStoreOrNot = true;
                            }
                            else
                            {
                                continue;
                            }*/
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
                                      "\n*** Device is disconnected ***" +
                                      "\n============================================\n");
                }
               
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

    }
}
