namespace united_movers_api.Models
{
    public class RiderAttachment
    {

        public int RiderID { get; set; }
        public int AttachmentTypeID { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentName { get; set; }
        public string ContentType { get; set; }
        public float NumberOfKB { get; set; }
        public byte[] Content { get; set; }
        public int LoggedInUserID { get; set; }

        public Guid AttachmentID { get; set; }
    }
}
