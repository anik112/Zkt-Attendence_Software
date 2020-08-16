using Oracle.DataAccess.Client;
using System;
using System.IO;
using System.Xml;

namespace ZktAttendence.Utilitis
{
    class DatabaseConnection
    {
        private static String filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\Setup.xml");
        private static String host = String.Empty;//"DESKTOP-NSLL7T5";"OFFICE-2"; // hosting pc name or ip address
        private static String serviceName = String.Empty; // service name or database name
        private static String userId = String.Empty; // username
        private static String password = String.Empty; // password
        private static String connectionString = String.Empty;



        private static void setupDatabase()
        {
            // get the node data from xml file
            XmlNodeList list = new SetupUtility().getDatabaseSetupInformation(filePath, "setup_database", "server_1");
            // set geting data in variable
            foreach (XmlNode node in list)
            {
                host = node.SelectSingleNode("host").InnerText;
                serviceName = node.SelectSingleNode("service_name").InnerText;
                userId = node.SelectSingleNode("user_id").InnerText;
                password = node.SelectSingleNode("password").InnerText;
            }

            // make TNS connection text
            connectionString = "Data Source=( DESCRIPTION ="
            + "(ADDRESS = (PROTOCOL = TCP)(HOST = " + host + ")(PORT = 1521))"
            + "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " + serviceName + "))"
            + ");User ID=" + userId + ";Password=" + password + "; Pooling=False;";
        }



        public static OracleConnection getConnection()
        {
            setupDatabase();
            try
            {
                // call Oracle Database connection driver
                OracleConnection con = new OracleConnection();
                con.ConnectionString = connectionString; // set connection string
                con.Open(); // request to connection
                Console.WriteLine("***** Connected With Oracle Server, Version: " + con.ServerVersion + " *****");
                return con;

            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseConnection sys: " + e.Message);
                Console.ReadLine();
            }
            return new OracleConnection();
        }



        public static OracleConnection getConnectionWithoutMsg()
        {
            setupDatabase();
            try
            {
                // call Oracle Database connection driver
                OracleConnection con = new OracleConnection();
                con.ConnectionString = connectionString; // set connection string
                con.Open(); // request to connection
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine("DatabaseConnection sys: " + e.Message);
                Console.ReadLine();
            }

            return new OracleConnection();
        }

    }
}
