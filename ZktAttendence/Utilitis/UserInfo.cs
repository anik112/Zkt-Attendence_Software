using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZktAttendence.Utilitis
{
    class UserInfo
    {
        public long machineNumber { get; set; }
        public long enrollNumber { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public long privilege { get; set; }
        public bool enable { get; set; }
    }
}
