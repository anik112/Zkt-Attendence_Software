using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace ZktAttendence.Utilitis
{
    class DatabaseConnection
    {

        private static String host = "new-PC";//"DESKTOP-NSLL7T5";"OFFICE-2"; // hosting pc name or ip address
        private static String serviceName = "payroll"; // service name or database name
        private static String userId = "payroll"; // username
        private static String password = "payroll"; // password

        // make TNS connection text
        private static String connectionString = "Data Source=( DESCRIPTION ="
        + "(ADDRESS = (PROTOCOL = TCP)(HOST = " + host + ")(PORT = 1521))"
        + "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " + serviceName + "))"
        + ");User ID=" + userId + ";Password=" + password + "; Pooling=False;";


        public static OracleConnection getConnection()
        {
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
                Console.WriteLine("DatabaseConnection sys: "+e.Message);
                Console.ReadLine();
            }

            return new OracleConnection();
        }



        public static OracleConnection getConnectionWithoutMsg()
        {
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
                Console.WriteLine("DatabaseConnection sys: "+e.Message);
                Console.ReadLine();
            }

            return new OracleConnection();
        }

    }
}
