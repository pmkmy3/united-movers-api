namespace united_movers_api.Models
{
    public class ChangePasswordRequest
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SecurityQuestion { get; set; }
    }
}
