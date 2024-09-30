using Lab1.Data;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services;
using Lab1.Services.IServices;
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
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(AdminDTO admin)
        {
           
            if (admin.Password == null || admin.UserName == null)
            {
                return BadRequest("Input cannot be null");
            }
            

            await _adminServices.RegisterAdminAsync(admin);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(AdminDTO loginAdmin)
        {
            var admin = await _adminServices.GetAdminByUserNameAsync(loginAdmin.UserName);

            if (admin == null)
            {
                return BadRequest("Admin account not found");
            }

            var token = await _adminServices.AdminLoginAsync(loginAdmin);

            return Ok(new { token });
        }      
    }
}
