using united_movers_api.Models;

namespace united_movers_api.Models
{
    public class ActivateOrDeactivateRiderRequest
    {
        public int RiderID { get; set; }
        public bool ActivateRider { get; set; }
        public int LoggedInUser { get; set; }
        public string Comments { get; set; }

        public DateTime? ActivationDate { get; set; }
    }
}
