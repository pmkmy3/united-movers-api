namespace united_movers_api.Models
{
    public class ContactInformation
    {
        public int EmployeeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AlternativeContactNumber { get; set; }
        public string AlternativeEmail { get; set; }
        public string City { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactPersonID { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string HighestDegreeEarned { get; set; }
        public string Landmark { get; set; }
        public string PreviousOrgName { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
