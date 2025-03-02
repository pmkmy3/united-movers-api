namespace united_movers_api.Models
{
    public class AddEmployeeAttachment 
    {
        public int EmpID { get; set; }
        public int AttachmentTypeID { get; set; }
        public int ReportTypeID { get; set; }
        public float NumberOfKB { get; set; }
        public byte[] Resource { get; set; }
        public string Tags { get; set; }
        public string ContentType { get; set; }
        public int LoggedInUserID { get; set; }
    }
}
