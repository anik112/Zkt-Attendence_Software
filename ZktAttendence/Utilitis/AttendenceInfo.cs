using System;

namespace ZktAttendence.Utilitis
{
    class AttendenceInfo
    {
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }
        public string DateTimeRecord { get; set; }
        public String empName { get; set; }
        public int privilege { get; set; }
        public bool enabled { get; set; }

        public DateTime DateOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("yyyy-MM-dd")); }
        }
        public DateTime TimeOnlyRecord
        {
            get { return DateTime.Parse(DateTime.Parse(DateTimeRecord).ToString("hh:mm:ss tt")); }
        }

        public String getIndRegID()
        {
            return this.IndRegID.ToString();
        }
    }
}
