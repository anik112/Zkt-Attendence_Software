using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using ZktAttendence.Utilitis;

namespace ZktAttendence.Core
{
    class UpdateInDatabase
    {
        private OracleConnection connection = DatabaseConnection.getConnection();

        

        public void getUserInfoFromDatabase()
        {
            try { 
                // make command object
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection; // set connection in command object
                cmd.CommandText = "SELECT empname FROM TB_PERSONAL_INFO where active=1"; // make sql
                OracleDataReader oracleDataReader = cmd.ExecuteReader(); // execute command in oracle database
                int count = 0;
                
                // loop for get data from oracleDataReader.
                // oracleDataReader.Read() function work for trevel last untill element in array.
                while (oracleDataReader.Read())
                {
                    /* OracleCommand command = new OracleCommand();
                     command.Connection = connection;
                     command.CommandText = "INSERT INTO CHECKS(NAME) VALUES ('" + oracleDataReader.GetString(0) + "')";
                     command.ExecuteNonQuery();*/
                    //Console.WriteLine(oracleDataReader.GetString(0)); // write data in console
                    //if(oracleDataReader.FetchSize)
                    count++;
                    Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b");
                    Console.Write("Process: " + count + "%");
                }
                cmd.Dispose();
                connection.Close();
                Console.WriteLine("\nSize= " + count); 
                Console.ReadLine();

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }



        public void storeLogDataInDatabase(int machineNumber, String userId, String timeDate, OracleConnection oraCon)
        {
            try
            {
                OracleCommand oraCommand = new OracleCommand(); // call oracle command object
                oraCommand.Connection = oraCon; // set oracle database connection in oracle command object
                // make sql string
                String prepareSql = "INSER INTO ZKT_ATTENDENCE_LOG(MACHINE_NUMBER,USER_ID,TIME_DATE) VALUES (" + machineNumber + ","
                                                                                                                + "'" + userId + "',"
                                                                                                                + "'" + timeDate + "')";
                oraCommand.CommandText = prepareSql; 
                int check = oraCommand.ExecuteNonQuery();
                oraCommand.Dispose();
                oraCon.Close(); // close oracle database connection
                Console.WriteLine(check);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }


    }
}
