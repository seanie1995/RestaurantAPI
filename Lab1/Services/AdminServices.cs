using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab1.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IConfiguration _configuration;

        public AdminServices(IAdminRepo adminRepo, IConfiguration configuration)
        {
            _adminRepo = adminRepo;
            _configuration = configuration;
        }

        public async Task<string> AdminLoginAsync(AdminDTO loginAdmin)
        {
            var existingAdmin = await _adminRepo.GetAdminByUsername(loginAdmin.UserName);

            if (existingAdmin == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, existingAdmin.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            var token = GenerateJwtToken(existingAdmin);

            return token;

        }

        public async Task RegisterAdminAsync(AdminDTO admin)
        {
            var existingAdmin = await _adminRepo.GetAdminByUsername(admin.UserName);

            if (existingAdmin != null)
            {
                throw new Exception("Username already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            var newAdmin = new Admin
            {
                UserName = admin.UserName,
                PasswordHash = passwordHash,
            };

            await _adminRepo.RegisterAdminAsync(newAdmin);

        }

        public async Task<Admin> GetAdminByUserNameAsync(string username)
        {
            var admin = await _adminRepo.GetAdminByUsername(username);

            if (admin == null)
            {
                throw new Exception("User not found");
            }

            return admin;
        }
        private string GenerateJwtToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, $"{admin.UserName}"),
                new Claim(ClaimTypes.Role, "Admin")
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
