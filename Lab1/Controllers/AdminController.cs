using Lab1.Data;
using Lab1.Models;
using Lab1.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly RestaurantContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(RestaurantContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public IActionResult Register(AdminDTO admin)
        {
            var existingAdmin = _context.Admin.SingleOrDefault(u => u.UserName == admin.UserName);

            if (existingAdmin != null)
            {
                return BadRequest("Already in use");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            var newAdmin = new Admin
            {
                UserName = admin.UserName,
                PasswordHash = passwordHash
            };

            _context.Admin.Add(newAdmin); 
            _context.SaveChanges(); 

            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(AdminDTO loginAdmin)
        {

            var admin = _context.Admin.SingleOrDefault(a => a.UserName == loginAdmin.UserName);

            if (loginAdmin == null || !BCrypt.Net.BCrypt.Verify(loginAdmin.Password, admin.PasswordHash))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(admin);

            return Ok(new {token});
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
