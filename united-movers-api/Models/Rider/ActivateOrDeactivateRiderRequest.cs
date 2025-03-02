using united_movers_api.Models;

namespace united_movers_api.Models
{
    public class ActivateOrDeactivateRiderRequest
    {
        public int RiderID { get; set; }
        public bool ActivateRider { get; set; }
        public string LoggedInUser { get; set; }
        public string Comments { get; set; } 
    }
}
