using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using united_movers_api.Models;
using united_movers_api.Repositories;

namespace united_movers_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;

        private readonly String _salt = "your-salt-123456789";
        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            this._authRepository = authRepository;
        }

        static string HashPassword(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8; // Number of threads
                argon2.MemorySize = 65536; // Memory usage in KB
                argon2.Iterations = 4; // Number of iterations

                return Convert.ToBase64String(argon2.GetBytes(32)); // Hash output
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            request.Password = HashPassword(request.Password, Encoding.UTF8.GetBytes(_salt));

            var reader = await this._authRepository.AuthenticateAsync(request);
            if (reader == null)
            {
                throw new Exception("Invalid username or password");
            }

            var userName = reader["UserName"] != null ? reader["UserName"].ToString() : "";
            var fName = reader["FirstName"] != null ? reader["FirstName"].ToString() : "";
            var roles = reader["Roles"] != null ? reader["Roles"].ToString().Split(',').ToList() : new List<string>();

            LoginResponse? res = new LoginResponse(userName, fName, true, roles) { };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim("UserId", res.UserId.ToString()),
                new Claim(ClaimTypes.Name, res.UserName)
            };
            foreach (var role in res.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            res.Token = tokenHandler.WriteToken(token);
            res.IsActive = true;

            return res;
        }

        public bool ChangePassword(ChangePasswordRequest request)
        {
          

            request.NewPassword = HashPassword(request.NewPassword, Encoding.UTF8.GetBytes(_salt));
            request.OldPassword = HashPassword(request.OldPassword, Encoding.UTF8.GetBytes(_salt));

            //request.NewPassword = Argon2.Hash(request.NewPassword);
            //request.OldPassword = Argon2.Hash(request.OldPassword);

            return this._authRepository.ChangePassword(request);
        }

        public bool ForgotPassword(ChangePasswordRequest request)
        {
            request.NewPassword = HashPassword(request.NewPassword, Encoding.UTF8.GetBytes(_salt));
            //request.NewPassword = Argon2.Hash(request.NewPassword);
            return this._authRepository.ForgotPassword(request);
        }
    }
}
