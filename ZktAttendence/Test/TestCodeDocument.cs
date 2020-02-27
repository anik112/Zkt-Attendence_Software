/*// =========== Need Document from Database Connection ==============
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



/* ======================= From Update in Database file =====================
 * 
 *              // make sql string
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

/* OracleCommand command = new OracleCommand();
     command.Connection = connection;
     command.CommandText = "INSERT INTO CHECKS(NAME) VALUES ('" + oracleDataReader.GetString(0) + "')";
     command.ExecuteNonQuery();*/
//Console.WriteLine(oracleDataReader.GetString(0)); // write data in console
//if(oracleDataReader.FetchSize)*



/*=========================== From Program file =========================
 *  
 * ICollection<MachineSelector> getList= new UpdateInDatabase().getMachineListFromDatabase(DatabaseConnection.getConnection());

       foreach(MachineSelector selector in getList)
       {
           Console.WriteLine("Machine Number: " + selector.getMachineNumber());
           Console.WriteLine("Ip Address: " + selector.getIpAddress());
           Console.WriteLine("Port Number: " + selector.getPortNumber());
       }*/
/*            new UpdateInDatabase().storeLogDataInDatabase(101,"0000006210","05:30:00 PM",DatabaseConnection.getConnection());
            new UpdateInDatabase().setMachineInfoIntoDatabase(101,"192.168.0.20",2470,DatabaseConnection.getConnection());*/

/* String tempFromDate = "20022020";
 String tempToDate = "23012020";
 String str = "Hello I am anik, Date: 01/20/2020 15:20:00,  02/24/2020 01:20:00,  01/23/2020 10:20:00";

 String workFromDay = tempFromDate.Substring(2, 2) + "/" + tempFromDate.Substring(0, 2) + "/" + tempFromDate.Substring(4, 4);
 String workToDay = tempToDate.Substring(2, 2) + "/" + tempToDate.Substring(0, 2) + "/" + tempToDate.Substring(4, 4);


 Console.WriteLine(workFromDay);
 Console.WriteLine(workToDay);

 if (str.Contains(workToDay))
 {
     Console.WriteLine("String Present");
 }
 else
 {
     Console.WriteLine("Not Present");
 }


 Console.ReadLine();*/
