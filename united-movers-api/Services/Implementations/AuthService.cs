using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using united_movers_api.Common;
using united_movers_api.Models;
using united_movers_api.Repositories;

namespace united_movers_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly string _salt;

        public AuthService(IConfiguration configuration, IAuthRepository authRepository)
        {
            _configuration = configuration;
            this._authRepository = authRepository;
            _salt = _configuration["Secret:SaltSecretKey"] ?? throw new ArgumentNullException(nameof(_configuration), "SaltSecretKey cannot be null");
        }

        

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            request.Password = Utils.HashPassword(request.Password, Encoding.UTF8.GetBytes(_salt));

            var reader = await this._authRepository.AuthenticateAsync(request);
            if (reader == null)
            {
                throw new Exception("Invalid username or password");
            }

            var userName = reader["UserName"] != null ? reader["UserName"].ToString() : "";
            var fName = reader["FirstName"] != null ? reader["FirstName"].ToString() : "";
            var roles = reader["Roles"] != null ? reader["Roles"]?.ToString()?.Split(',').ToList() : new List<string>();

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
            request.NewPassword = Utils.HashPassword(request.NewPassword, Encoding.UTF8.GetBytes(_salt));
            request.OldPassword = Utils.HashPassword(request.OldPassword, Encoding.UTF8.GetBytes(_salt));
            return this._authRepository.ChangePassword(request);
        }

        public bool ForgotPassword(ChangePasswordRequest request)
        {
            request.NewPassword = Utils.HashPassword(request.NewPassword, Encoding.UTF8.GetBytes(_salt));
            return this._authRepository.ForgotPassword(request);
        }
    }
}
