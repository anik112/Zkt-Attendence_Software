using System;

namespace ZktAttendence.Utilitis
{
    class AttendenceInfo
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }

        public DateTime DateOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("MM/dd/yyyy")); }
        }
        public DateTime TimeOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("HH:mm:ss")); }
        }

        public string getIndRegID()
        {
            return this.IndRegID.ToString();
        }
    }
}
