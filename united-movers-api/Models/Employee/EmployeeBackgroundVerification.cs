namespace united_movers_api.Models
{
    public class EmployeeBackgroundVerification
    {
        public int EmployeeID { get; set; }
        public bool IsBackgroundVerificationCompleted { get; set; }
        public bool IsPhysicalVerificationCompleted { get; set; }
        public string BackgroundVerificationAgencyName { get; set; }
    }
}
