using System;

namespace ZktAttendence.Utilitis
{
    class MachineSelector
    {
        private int machineNumber = 0;
        private String ipAddress = String.Empty;
        private int portNumber = 0;
        private int comPass = 0;

        public int getMachineNumber()
        {
            return this.machineNumber;
        }


        public void setMachineNumber(int number)
        {
            this.machineNumber = number;
        }

        public String getIpAddress()
        {
            return this.ipAddress;
        }

        public void setIpAddress(String address)
        {
            this.ipAddress = address;
        }

        public int getPortNumber()
        {
            return this.portNumber;
        }

        public void setPortNumber(int number)
        {
            this.portNumber = number;
        }

        public int getcomPass()
        {
            return this.comPass;
        }

        public void setcomPass(int password)
        {
            this.comPass = password;
        }
    }
}
