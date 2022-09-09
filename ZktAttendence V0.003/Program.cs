using System;
using System.Drawing.Text;
using System.IO;
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

        //private static String zktFilePath = "D:\ATT_SOFT\SetupMachineList.xml";
        //private static String dbaFilePath = "D:\ATT_SOFT\Setup.xml";

        static void Main(string[] args)
        {
            new Master().DriverMethod();

            /*while (true)
            {
                Console.WriteLine(Cursor.Position.ToString());
                Thread.Sleep(600);
            }*/

            //Application.EnableVisualStyles();
            //Application.Run(new WindowFrom(zktFilePath));
            //Console.ReadLine();
        }

    }
}


/*
            new Master().consoleProcessForAttendence();
            Console.WriteLine("\n\n#########  Please type Enter & Close  ###########" +
                                "\n     @ 2019-Vistasoft IT Bangladesh Ltd.  ");
            */
