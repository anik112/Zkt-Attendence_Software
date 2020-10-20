namespace ZktAttendence.Utilitis
{
    class UserInfo
    {
        public int machineNumber { get; set; }
        public int dwEnrollNumber { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int privilege { get; set; }
        public bool enable { get; set; }


        public string getDwEnrollNumber()
        {
            return this.dwEnrollNumber.ToString();
        }
    }
}
