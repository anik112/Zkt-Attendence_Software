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


            new UpdateInDatabase().storeLogDataInDatabase(101,"000101","12:30",DatabaseConnection.getConnection());


            Console.ReadLine();
        } 
    }
}

