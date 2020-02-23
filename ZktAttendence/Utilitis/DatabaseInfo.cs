using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZktAttendence.Utilitis
{
    class DatabaseInfo
    {

        private decimal sl = 0;
        private String hostName = String.Empty;
        private String serviceName = String.Empty;
        private String userId = String.Empty;
        private String pCode = String.Empty;
        private String protocol = String.Empty;
        private decimal port = 0;
        private String server = String.Empty;


        public decimal getSl()
        {
            return this.sl;
        }

        public void setSl(int sl)
        {
            this.sl = sl;
        }

        public String getHostName()
        {
            return this.hostName;
        }

        public void setHostName(String hostName)
        {
            this.hostName = hostName;
        }

        public String getServiceName()
        {
            return this.serviceName;
        }

        public void setServiceName(String serviceName)
        {
            this.serviceName = serviceName;
        }

        public String getUserId()
        {
            return this.userId;
        }
        public void setUserid(String userId)
        {
            this.userId = userId;
        }
        public String getPCode()
        {
            return this.pCode;
        }

        public void setPCode(String pCode)
        {
            this.pCode = pCode;
        }

        public String getProtocol()
        {
            return this.protocol;
        }

        public void setProtocol(String protocol)
        {
            this.protocol = protocol;
        }

        public decimal getPort()
        {
            return this.port;
        }

        public void setPort(int port)
        {
            this.port = port;
        }

        public String getServer()
        {
            return this.server;
        }

        public void setServer(String server)
        {
            this.server = server;
        }
    }
}
