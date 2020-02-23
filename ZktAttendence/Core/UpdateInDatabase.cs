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
        
        /**
         * this method use for get user information from database.
         */
        public void getUserInfoFromDatabase(OracleConnection connection)
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
                    Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"); // remove text from console
                    Console.Write("Process: " + count + " Data"); // write text in console
                }
                cmd.Dispose(); // close OracleCommand
                connection.Close(); // close Connection
                Console.WriteLine("\nSize= " + count); 
                Console.ReadLine();

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }


        /**
         * This method use for store attendence log information in database.
         */
        public void storeLogDataInDatabase(decimal machineNumber, String userId, String timeDate, OracleConnection oraCon)
        {
            try
            {
                String prepareSql = "INSERT INTO ZKT_ATTENDENCE_LOG(MACHINE_NUMBER,USER_ID,TIME_DATE) VALUES ("+machineNumber+",'"+userId+"','"+timeDate+"')";
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand.Connection = oraCon;
                oracleCommand.CommandText = prepareSql;
                Console.WriteLine(oracleCommand.CommandText);
                int check=oracleCommand.ExecuteNonQuery();
                oracleCommand.Dispose();
                oraCon.Close();

                if (check > 0)
                {
                    Console.WriteLine("Data Inserted ...");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        /**
         * this method use for get machine information from daatabase.
         */
        public ICollection<MachineSelector> getMachineListFromDatabase(OracleConnection oraConn)
        {

            ICollection<MachineSelector> machineList = new List<MachineSelector>();
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = oraConn;
                String prepareSql = "SELECT MACHINE_NUMBER,IP_ADDRESS,PORT_NUMBER FROM ZKT_MACHINE_INFO";
                command.CommandText = prepareSql;
                OracleDataReader oracleData = command.ExecuteReader();

                while (oracleData.Read())
                {
                    MachineSelector selector = new MachineSelector();
                    selector.setMachineNumber((int)oracleData.GetDecimal(0));
                    selector.setIpAddress(oracleData.GetString(1));
                    selector.setPortNumber((int)oracleData.GetDecimal(2));

                    machineList.Add(selector);
                }

                return machineList;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

            return machineList;
        }

        /**
         * this method work for store data in database.
         */
        public void setMachineInfoIntoDatabase(int machineNumber,String ipAddress, int portNumber, OracleConnection oraCon)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = oraCon;
            String preparSql = "INSERT INTO ZKT_MACHINE_INFO(MACHINE_NUMBER,IP_ADDRESS,PORT_NUMBER) VALUES ("+machineNumber+",'"+ipAddress+"',"+portNumber+")";
            command.CommandText = preparSql;
            command.ExecuteNonQuery();
            oraCon.Close();
            command.Dispose();
        }

    }
}



/*              // make sql string
                String prepareSql = "INSERT INTO ZKT_ATTENDENCE_LOG (MACHINE_NUMBER,USER_ID,TIME_DATE) VALUES (:machineNum,:secNo,:timeDate);";
                Console.WriteLine(prepareSql);

                OracleCommand oraCommand = new OracleCommand(prepareSql); // call oracle command object
                oraCommand.Connection = oraCon; // set oracle database connection in oracle command object

                OracleParameter machineNum = new OracleParameter("machineNum", OracleDbType.Decimal);
                OracleParameter secNo = new OracleParameter("secNo", OracleDbType.Varchar2, 100);
                OracleParameter timeDates = new OracleParameter("timeDate", OracleDbType.Varchar2, 40);

                machineNum.Value = machineNumber;
                secNo.Value = userId;
                timeDates.Value = timeDate;

                oraCommand.Parameters.Add("machineNum",machineNum);
                oraCommand.Parameters.Add("secNo", secNo);
                oraCommand.Parameters.Add("timeDate",timeDates);

                int check = oraCommand.ExecuteNonQuery();
                oraCommand.Dispose();
                oraCon.Close(); // close oracle database connection
                Console.WriteLine(check);*/
