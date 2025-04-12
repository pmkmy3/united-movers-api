using System.ComponentModel.DataAnnotations;

namespace united_movers_api.Models
{
    public class LoginResponse
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<string>? Roles { get; set; }

        public bool IsActive { get; set; }
        public string? Token { get; set; }
        public bool IsTempPassword { get; set; }

        public LoginResponse() { }

        public LoginResponse(string userId, string userName, string name, bool isTempPassword, List<string>? roles)
        {
            UserId = userId;
            UserName = userName;
            FirstName = name;
            IsTempPassword = isTempPassword;
            Roles = roles;
        }
    }
}
