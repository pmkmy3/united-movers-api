namespace united_movers_api.Models
{
    public class RiderBackgroundVerification
    {
        public int RiderID { get; set; }
        public bool IsBackgroundVerificationCompleted { get; set; }
        public bool IsPhysicalVerificationCompleted { get; set; }
        public bool IsAadhaarVerified { get; set; }
        public bool IsContactNumberVerified { get; set; }
        public string FamilyMemberName { get; set; }
        public string FamilyMemberRelation { get; set; }
        public string FamilyMemberID { get; set; }
        public string FamilyMemberIDType { get; set; }
        public string FamilyMemberContact { get; set; }
        public string BackgroundVerificationAgencyName { get; set; }
    }
}
