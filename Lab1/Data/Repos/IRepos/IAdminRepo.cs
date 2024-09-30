using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface IAdminRepo
    {
        Task RegisterAdminAsync(Admin admin);
        Task<Admin> GetAdminByUsername(string userName);
    }
}
