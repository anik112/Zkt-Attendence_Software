using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZktAttendence.Core_Service;
using ZktAttendence.Core;
using zkemkeeper;
using ZktAttendence.Utilitis;
using System.Xml;

namespace ZktAttendence
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Master().DriverMethod();
            //new UpdateInDatabase().getUserInfoFromDatabase(DatabaseConnection.getConnection());
            new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\n#########  Please type Enter & Close  ###########" +
                                "\n@ 2019-Vistasoft IT Bangladesh Ltd. Dev-by-Pranta");
            Console.ReadLine();

        }
    }
}

