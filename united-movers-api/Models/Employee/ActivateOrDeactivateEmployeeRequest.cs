using united_movers_api.Models;

namespace united_movers_api.Models
{
    public class ActivateOrDeactivateEmployeeRequest
    {
        public int EmployeeID { get; set; }
        public bool ActivateEmployee { get; set; }
        public string LoggedInUser { get; set; }
        public string Comments { get; set; }
        public string Password { get; set; }
    }
}
