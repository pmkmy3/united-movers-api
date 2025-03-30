namespace united_movers_api.Models
{
    public class EmployeeAttachment
    {
        public int EmployeeID { get; set; }
        public int AttachmentTypeID { get; set; }


        public string AttachmentType { get; set; }
        public string AttachmentName { get; set; }
        public string ContentType { get; set; }
        public float NumberOfKB { get; set; }
        public string Content { get; set; }
        //public string Tags { get; set; }

        public int LoggedInUserID { get; set; }
        public Guid AttachmentID { get; set; }
    }
}
