namespace united_movers_api.Models
{
    public class ValidateAndCreateRiderIDRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AadhaarNumber { get; set; }
        public string PAN { get; set; }
        public string ContactNumber { get; set; }
        public string BloodGroup { get; set; }
        public string PersonalEmailID { get; set; }
        public int LoggedInUserID { get; set; }

        public int RiderID { get; set; }
    }

}
