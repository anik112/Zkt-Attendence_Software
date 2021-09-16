# Zkt-Attendence_Software
> Create a application for get attendence data from ZKT device.


# Programming Module
**Uses:** *Full documentation of this project*

**Folder Structur:**
- **root**
    - *bin*
    - *Core*
    - *Core Service*
    - *dll*
    - *Properties*
    - *Utilitis*
    - *view*
    - *RegSDK*

- **Main Execution File: Program.cs**
- **Configration File: App.config**
- **External Function Libray File: zkemkeeper.dll**
- **External Database Libray File: Oracle.DataAccess.dll**
- **Master File: Master.cs**


Prerequisite
============
- *Setup.xml*
- *SetupMachineList.xml*
- *Oracle.DataAccess.dll*
- *zkemkeeper.dll*
- *Microsoft .net framework 4.5*

#### First you need to register the SDK in your System. For register the SDK you simple ___Register_SDK.bat___ in ___RegSDK___ folder. you need to install ___Microsoft .net framework 4.5___
#### Set the computer time in HH:mm A/P & date in dd/MM/yyyy format.

Installation
============
- Install ___Microsoft framework 4.5 or higher___
- Install ___ZKT 5.0 Attendance Managemnet Software___
- Set the system date format to DD/MM/YYYY
- Add the device list in ___SetupMachineList.xls___ file
- Create a shortcut of Att.exe and past it on desktop
- Run the application


XML File Writting Rules
=======================

Please first open the ZKT 5.0 software. Add the device ip and check the connection is ok or not. After that we add the device ip in XML file.

```
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<deviceSetupInfo>
  <device0>
    <machineNo>121</machineNo>
    <ipAddress>192.168.1.201</ipAddress>
    <port>1515</port>
    <pass>0</pass>
  </device0>
  <device1>
    <machineNo>122</machineNo>
    <ipAddress>192.168.1.204</ipAddress>
    <port>1515</port>
    <pass>0</pass>
  </device1>
</deviceSetupInfo>
````
In this content we can see some tage which is ```<device0>```, ```<machineNo>``` , ```<ipAddress>``` , ```<port>```,```<pass>``` ;

- **```<device0>``` In this tage we write the device index. If we have 2 device then we can write:**
     for 1st device ```<device0> some code </device0>```; 
     for 2nd device ```<device1> some code </device1>```;
- **```<machineNo>``` In this tage we write the machine number of device. if we have 2 device the we can write:**
     for 1st device ```<machineNo>101</machineNo>```;
     for 2nd device ```<machineNo>102</machineNo>```;

- **```<ipAddress>``` In this tag we wite the device ip number. If we have 2 device then we can write:**
     for 1st device ```<ipAddress>192.168.1.201</ipAddress>```;
     for 2nd device ```<ipAddress>192.168.1.202</ipAddress>```;
- **```<port>``` In this tag we write device port. The default port is 1515.**
- **```<pass>``` In this tag we write device password. If device have any communication paswword.**


Content
=======
##### Workable Class name:
```javascript
// Main class
Program.cs
Master.cs
ConsoleViewV2.cs

// Core class
AttendenceDataWriteInTxt.cs
CoreZktClass.cs
CoreZkt.cs

// Utilitis class
AttendenceInfo.cs
MachineSelector.cs
SetupUtility.cs
UserInfo.cs

```

##### Workable Function name:
```javascript
// Mian function
Class: Program Fun: static void Main(String args[]);

// View function
Class: ConsoleViewV2 Fun: public ConsoleViewV2(String zktSetupPath, String setupPath);
Class: ConsoleViewV2 Fun: public void showConsole();

// Controller function
Class: AttendenceDataWriteInTxt Fun: public bool consoleProcessForAttendence(String zktSetupPath);
Class: CoreZkt Fun: public ICollection<AttendenceInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber);
Class: CoreZkt Fun: public bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo);
Class: CoreZkt Fun: string GetDeviceInformation(CZKEM objZkeeper, int machineNumber);
Class: CoreZkt Fun: ICollection<UserIdInfo> GetUserIdList(CZKEM objZkeeper, int machineNumber);
Class: CoreZkt Fun: List<UserInfo> GetUserInformation(CZKEM objCzkem, int machineNumber);
Class: CoreZkt Fun: ICollection<AttendenceInfo> GetAttendenceLogData(CZKEM objZkt, int machineNumber);
Class: CoreZkt Fun: int GetMachineNumber(CZKEM objZkt);

// Database function
Class: UpdateInDatabase Fun:  public void getUserInfoFromDatabase(OracleConnection connection);
Class: UpdateInDatabase Fun:  public void storeLogDataInDatabase(decimal machineNumber, String userId, String timeDate, OracleConnection oraCon);
Class: UpdateInDatabase Fun:  public ICollection<MachineSelector> getMachineListFromDatabase(OracleConnection oraConn);
Class: UpdateInDatabase Fun:  public void setMachineInfoIntoDatabase(int machineNumber, String ipAddress, int portNumber, OracleConnection oraCon);
Class: UpdateInDatabase Fun: public ICollection<String> getSelectedTimeDateFromDatabase(String date, OracleConnection oraCon);
Class: UpdateInDatabase Fun: public Boolean checkIfIsNotExists(String timeDate, OracleConnection oraCon);

// Utilitis function
Class: AttendenceInfo Fun:  public string getIndRegID();
Class: AttendenceInfo Fun:  public int getMachineNumber();
Class: AttendenceInfo Fun:  public void setMachineNumber(int number);
Class: AttendenceInfo Fun:  public String getIpAddress();
Class: AttendenceInfo Fun:  public int getPortNumber();
Class: AttendenceInfo Fun:  public void setPortNumber(int number);

Class: SetupUtility Fun: public void xmlWriter(String host, String serviceName, String userId, String password, XmlTextWriter xmlTextWriter);
Class: SetupUtility Fun: public void writeMachineInfoInXML(int machineNo, String ipAddress, int port, XmlTextWriter xmlTextWriter);
Class: SetupUtility Fun: public void writeZktFileLoc(String lodingPath, String storePath);
Class: SetupUtility Fun: public String getZktFilePath(String lodingPath);
Class: SetupUtility Fun: public List<MachineSelector> getDeviceSetupInformation(String filePath, String rootNode);
Class: SetupUtility Fun: public XmlNodeList getDatabaseSetupInformation(String filePath, String rootNode, String selectedSubNode);

Class: UserInfo Fun: public string getDwEnrollNumber();

```

Describe The Program
====================

- **Program.cs file is the main runable file where have: ** 
```
private static String zktFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "SetupMachineList.xml");
private static String dbaFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Setup.xml");
```





Discussion Of Functions
=======================

**1. static void Main(String args[]);**
###### This is main function of this application. This function load when we execute the program. Frist it get the XML file path by ' System.IO.Path.Combine(Environment.CurrentDirectory, "SetupMachineList.xml") ' this statements then run main view class.

**2. public ConsoleViewV2(String zktSetupPath, String setupPath);**
###### This is conustroctor of view class. This function get xml setup path by [ zktSetupPath, setupPath ]

**3. public void showConsole();**
###### This function show the text in windows console and get input from user. Fristly it show company information then call controller function.

**4. public bool consoleProcessForAttendence(String zktSetupPath);**
###### This function work for get attendence from device and write on txt file. Par [ _zktSetupPath_ ] for get setup file and return True if it's successfully write data on txt file other return false.

**5. public List < MachineSelector > getDeviceSetupInformation(String filePath, String rootNode);**
###### This function work for get Device setup information from XML file. In XML file have device number and ip address. This function take two prm [__filePath__] takes XML file location and [__rootNode__] takes prents node of XML and return a list of device info.

**6. public bool GetConnection(CZKEM cZKEM, string ipAddress, int portNo);**
###### This function check the connection computer with device. [___cZKME___] take object of root class, [___ipAddress___] take ip address of device, [___portNo___] take connection port number where computer send request. If it's work good then return true other false.

**7. public ICollection < AttendenceInfo > GetAttendenceLogData(CZKEM objZkt, int machineNumber);**
###### This function get attendence from device and return all of data in a collection. The prm [___objZkt___] take the root class object, [___machineNumber___] takes the device number who connected with computer.

**8. ICollection < UserIdInfo > GetUserIdList(CZKEM objZkeeper, int machineNumber);**
###### This function get all user id list from device and return by a collection. The prm [___objZkeeper___] take root class object, [___machineNumber___] takes the device number who connected with computer.

**9. List < UserInfo > GetUserInformation(CZKEM objCzkem, int machineNumber);**
###### This function get all user id list from device and return by a collection. The prm [___objZkeeper___] take root class object, [___machineNumber___] takes the device number who connected with computer.

**10. public void getUserInfoFromDatabase(OracleConnection connection);**
###### This function get user information from database ZKT_USER_INFO table and store in a array. The prm [___connection___] take Oracle database conenction object. 

**11. public void storeLogDataInDatabase(decimal machineNumber, String userId, String timeDate, OracleConnection oraCon);**
###### This function get attendence log data from array and store it in database ZKT_ATTENDENCE_LOG table. The prm [___machineNumber___] get device number, [___userId___] get current user id number, [___timeDate___] get attendence time and date, [___oraCon___] get Oracle connection object.

**12. public ICollection < MachineSelector > getMachineListFromDatabase(OracleConnection oraConn);**
###### This function get machine number and ip address from ZKT_MACHINE_INFO. The prm [___oraConn___] take Oracle connection.

**13. public void setMachineInfoIntoDatabase(int machineNumber, String ipAddress, int portNumber, OracleConnection oraCon);**
###### This function store machine information in ZKT_MACHINE_INFO table. The prm [___machineNumber___] take device number, [___ipAddress___] take device ip address, [___portNumber___] take device connection port number, [____oraCon___] take Oracle connection.

**14. public ICollection < String > getSelectedTimeDateFromDatabase(String date, OracleConnection oraCon);**
###### This function get attendence data from ZKT_ATTENDENCE_LOG by given date. prm [___date___] take searching date, [___oraCon___] take Oracle connection.

**15. public Boolean checkIfIsNotExists(String timeDate, OracleConnection oraCon);**
###### This function check get time already entry or not in database. prm [___timeDate___] take time and date which try to match in database, [___oraCon___] take Oracle connection.

**16. public string getIndRegID();**
###### This function return user REG id in String format.

**17. public int getMachineNumber();**
###### This functon return device number in int format.

**18.  public void setMachineNumber(int number);**
###### This function set device number.

**19. public String getIpAddress();**
###### This function return device ip address in String format.

**20. public int getPortNumber();**
###### This function return port number in int format.

**21. public void setPortNumber(int number);**
###### This function set port number.

**22. public void xmlWriter(String host, String serviceName, String userId, String password, XmlTextWriter xmlTextWriter);**
###### This function write database information in XML file. The prm [___host___] take oracle host address, [___serviceName___] take oracle service name, [___userId___] take Oracle user name, [___password___] take user password, [___xmlTextWriter___] take object of text writer.

**23. public void writeMachineInfoInXML(int machineNo, String ipAddress, int port, XmlTextWriter xmlTextWriter);**
###### This function write device information in XML file. The prm [___machineNo___] take device number, [___ipAddress___] take device ip address, [___port___] take port number, [___xmlTextWriter___] take xml writer object.

**24. public void writeZktFileLoc(String lodingPath, String storePath);**
###### This function write file path in XML file. [____lodingPath___] take root node of XML, [___storePath___] take ZKT db file path.

**25. public String getZktFilePath(String lodingPath);**
###### This function return ZKT db file path from XML file. The prm [___lodingPath___] take root node of XML file.

**26. public List < MachineSelector > getDeviceSetupInformation(String filePath, String rootNode);**
###### This function return a list of device information from XML file. [___filePath___] take XML file location, [___rootNode___] take root node.

**27. public XmlNodeList getDatabaseSetupInformation(String filePath, String rootNode, String selectedSubNode);**
###### This function return a XML node list from XML file. [___filePath___] take XML file location, [___rootNode___] take root node name, [___selectedSubNode___] take sub node where information store.

Application Output
==================

**Output 01:** 
##### 105:00020001990:20191125:075438:BLANK !!:11
**Output 02:** 
##### 105:00020001990:20191125:075438

**Explain the output**
```javascript

105 -> This is machine number
: -> Notation
00020001990 ->  Cardno or sec no
: -> Notation
20191125 -> Date by (Year Month Day)
: -> Notation 
075438 -> Time (HH mm ss)
: -> Notation
BLANK !! -> Notation
: -> Notation
11 -> Notation

````

# Follow me on
#### Github: https://github.com/anik112
#### Facebook: www.facebook.com/paulanik112/
