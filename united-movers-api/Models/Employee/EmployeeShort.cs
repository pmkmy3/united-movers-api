namespace united_movers_api.Models
{
    public class EmployeeShort
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalEmailID { get; set; }

        public string ContactNumber { get; set; }

        public string AadhaarNumber { get; set; }

        public string PanNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
