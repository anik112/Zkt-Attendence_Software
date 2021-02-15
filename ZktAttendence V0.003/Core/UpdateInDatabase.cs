using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
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
            try
            {
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
                    count++;
                    Console.Write("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b"); // remove text from console
                    Console.Write("Process: " + count + " Data"); // write text in console
                }
                cmd.Dispose(); // close OracleCommand
                connection.Close(); // close Connection
                Console.WriteLine("\nSize= " + count);
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
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
                String prepareSql = "INSERT INTO ZKT_ATTENDENCE_LOG(MACHINE_NUMBER,USER_ID,TIME_DATE) VALUES (" + machineNumber + ",'" + userId + "','" + timeDate + "')";
                OracleCommand oracleCommand = new OracleCommand();
                oracleCommand.Connection = oraCon;
                oracleCommand.CommandText = prepareSql;
                //Console.WriteLine(oracleCommand.CommandText);
                int check = oracleCommand.ExecuteNonQuery();
                oracleCommand.Dispose();

                /*if (check > 0)
                {
                    Console.WriteLine("Data Inserted ...");
                }*/

            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
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
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
                Console.ReadLine();
            }

            return machineList;
        }

        /**
         * this method work for store data in database.
         */
        public void setMachineInfoIntoDatabase(int machineNumber, String ipAddress, int portNumber, OracleConnection oraCon)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = oraCon;
                String preparSql = "INSERT INTO ZKT_MACHINE_INFO(MACHINE_NUMBER,IP_ADDRESS,PORT_NUMBER) VALUES (" + machineNumber + ",'" + ipAddress + "'," + portNumber + ")";
                command.CommandText = preparSql;
                command.ExecuteNonQuery();
                oraCon.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
                Console.ReadLine();
            }
        }


        /**
         * This method work for take existing date list from databse
         */
        public ICollection<String> getSelectedTimeDateFromDatabase(String date, OracleConnection oraCon)
        {
            ICollection<String> listOfDate = new List<String>();

            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = oraCon;
                String preparSql = "SELECT TIME_DATE FROM ZKT_ATTENDENCE_LOG WHERE TIME_DATE LIKE '" + date + " %'";
                command.CommandText = preparSql;
                OracleDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    listOfDate.Add(dataReader.GetString(0));
                }
                command.Dispose();
                oraCon.Close();

                return listOfDate;
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
                Console.ReadLine();
            }
            return listOfDate;
        }


        public Boolean checkIfIsNotExists(String timeDate, OracleConnection oraCon)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = oraCon;
                String preparSql = "SELECT TIME_DATE FROM ZKT_ATTENDENCE_LOG WHERE TIME_DATE='" + timeDate + "'";
                command.CommandText = preparSql;
                OracleDataReader dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    command.Dispose();
                    return false;
                }
                else
                {
                    command.Dispose();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("UpdateInDatabase sys: " + e.Message);
                Console.ReadLine();
            }

            return false;
        }

    }
}
