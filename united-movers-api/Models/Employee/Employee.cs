namespace united_movers_api.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string PersonalEmailID { get; set; }
        public string ContactNumber { get; set; }
        public string AlternativeContactNumber { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string AadhaarNumber { get; set; }
        public string PanNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankIFSCCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedByID { get; set; }
        public int ModifiedByID { get; set; }
        public string AlternativeEmail { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactPersonID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Landmark { get; set; }
        public string HighestDegreeEarned { get; set; }
        public string PreviousOrgName { get; set; }
        public string UANNumber { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string InsurerName { get; set; }
        public DateTime? InsuranceStartDate { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public bool IsBackgroundVerificationCompleted { get; set; }
        public bool IsPhysicalVerificationCompleted { get; set; }
        public string BackgroundVerificationAgencyName { get; set; }
    }
}
