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
Class: ConsoleViewV2 Fun:  public void showConsole();

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
=========

**1. static void Main(String args[]);**
###### This is main function of this application. This function load when we execute the program. Frist it get the XML file path by ' System.IO.Path.Combine(Environment.CurrentDirectory, "SetupMachineList.xml") ' this statements then run main view class.



