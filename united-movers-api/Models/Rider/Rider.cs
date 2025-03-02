namespace united_movers_api.Models
{
    public class Rider
    {
        public int RiderID { get; set; }
        public int VendorID { get; set; }
        public string ReferenceName { get; set; }
        public string RiderName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AadharCardNumber { get; set; }
        public string PANNumber { get; set; }
        public string BloodGroup { get; set; }
        public string ContactNumber { get; set; }
        public string EmailID { get; set; }
        public string AlternativeContactNumber { get; set; }
        public string AlternativeEmail { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactPersonID { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Landmark { get; set; }
        public string HighestDegreeEarned { get; set; }
        public string PreviousOrgName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string UANNumber { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string InsurerName { get; set; }
        public DateTime? InsuranceStartDate { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public string FamilyMemberName { get; set; }
        public string FamilyMemberRelation { get; set; }
        public string FamilyMemberIDType { get; set; }
        public string FamilyMemberID { get; set; }
        public string FamilyMemberContact { get; set; }
        public bool? IsBackgroundVerificationCompleted { get; set; }
        public bool? IsPhysicalVerificationCompleted { get; set; }
        public string BackgroundVerificationAgencyName { get; set; }
        public bool? IsAadhaarVerified { get; set; }
        public bool? IsContactNumberVerified { get; set; }
        public string AdditionalNotes { get; set; }
    }

}
