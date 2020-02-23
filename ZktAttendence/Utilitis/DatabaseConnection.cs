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
        private static String host = "new-PC";//"DESKTOP-NSLL7T5"; // hosting pc name or ip address
        private static String serviceName = "payroll"; // service name or database name
        private static String userId = "payroll"; // username
        private static String password = "payroll"; // password

        // make TNS connection text
        private static String connectionString = "Data Source=( DESCRIPTION ="
        + "(ADDRESS = (PROTOCOL = TCP)(HOST = " + host + ")(PORT = 1521))"
        + "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = " + serviceName + "))"
        + ");User ID=" + userId + ";Password=" + password + ";";


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

                /*
                // =========== Need Document ==============
                // make command object
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con; // set connection in command object
                cmd.CommandText = "SELECT empname FROM TB_PERSONAL_INFO"; // make sql
                OracleDataReader oracleDataReader = cmd.ExecuteReader(); // execute command in oracle database
                // loop for get data from oracleDataReader.
                // oracleDataReader.Read() function work for trevel last untill element in array.
                while (oracleDataReader.Read())
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = con;
                    command.CommandText = "INSERT INTO CHECKS(NAME) VALUES ('"+oracleDataReader.GetString(0)+"')";
                    command.ExecuteNonQuery();
                    Console.WriteLine(oracleDataReader.GetString(0)); // write data in console
                }
                Console.ReadLine();
                */

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            return new OracleConnection();
        }
    }
}
