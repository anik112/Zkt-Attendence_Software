using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZktAttendence.Core_Service;
using ZktAttendence.Core;
using zkemkeeper;
using ZktAttendence.Utilitis;


namespace ZktAttendence
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Master().DriverMethod();
            //new UpdateInDatabase().getUserInfoFromDatabase(DatabaseConnection.getConnection());
            /*Console.WriteLine("Please type any key... to start");
            Console.ReadLine();
            new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\nPlease type any key... close");
            Console.ReadLine();*/

            /*ICollection<MachineSelector> getList= new UpdateInDatabase().getMachineListFromDatabase(DatabaseConnection.getConnection());

            foreach(MachineSelector selector in getList)
            {
                Console.WriteLine("Machine Number: " + selector.getMachineNumber());
                Console.WriteLine("Ip Address: " + selector.getIpAddress());
                Console.WriteLine("Port Number: " + selector.getPortNumber());
            }*/
            /*            new UpdateInDatabase().storeLogDataInDatabase(101,"0000006210","05:30:00 PM",DatabaseConnection.getConnection());
                        new UpdateInDatabase().setMachineInfoIntoDatabase(101,"192.168.0.20",2470,DatabaseConnection.getConnection());*/

            String tempFromDate = "02202020";
            String tempToDate = "01022020";

            String workFromDay = tempFromDate.Substring(3, 4) + "/" + tempFromDate.Substring(1, 2) + "/" + tempFromDate.Substring(4, 7);
            String workToDay = tempToDate.Substring(3, 4) + "/" + tempToDate.Substring(1, 2) + "/" + tempToDate.Substring(4, 7);

            Console.WriteLine(workFromDay);
            Console.WriteLine(workToDay);

            Console.ReadLine();
        } 
    }
}

