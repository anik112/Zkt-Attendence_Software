using System;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using ZktAttendence.Test;
using ZktAttendence.Utilitis;

namespace ZktAttendence
{
    class Program
    {

        /*private static String zktFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\SetupMachineList.xml");
        private static String dbaFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\Setup.xml");*/

        private static String zktFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "SetupMachineList.xml"); // call ZKT setup file
        private static String dbaFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Setup.xml"); // call others setup file.

        static void Main(string[] args)
        {
            // new ZktAttendence.view.ConsoleViewV2(zktFilePath, dbaFilePath).showConsole();


            while (true)
            {
                Console.WriteLine(Cursor.Position.ToString());
                Thread.Sleep(600);
            }
            
            Console.ReadLine();
        }

    }
}


/*
            new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\n#########  Please type Enter & Close  ###########" +
                                "\n     @ 2019-Vistasoft IT Bangladesh Ltd.  ");
            */
