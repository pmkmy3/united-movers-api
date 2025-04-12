using System.Data;
using united_movers_api.Models;

namespace united_movers_api.Repositories
{
    public interface IAuthRepository
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest request);

        bool ChangePassword(ChangePasswordRequest request);

        bool ForgotPassword(ChangePasswordRequest request);
    }
}
