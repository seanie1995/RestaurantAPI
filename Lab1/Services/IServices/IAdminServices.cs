using Lab1.Models;
using Lab1.Models.DTOs;

namespace Lab1.Services.IServices
{
    public interface IAdminServices
    {
        Task RegisterAdminAsync(AdminDTO admin);
        Task<string> AdminLoginAsync(AdminDTO admin);

        Task<Admin> GetAdminByUserNameAsync(string userName);
    }
}
