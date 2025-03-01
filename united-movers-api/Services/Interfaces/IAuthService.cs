using united_movers_api.Models;

namespace united_movers_api.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);

        bool ChangePassword(ChangePasswordRequest request);

        bool ForgotPassword(ChangePasswordRequest request);
    }
}
