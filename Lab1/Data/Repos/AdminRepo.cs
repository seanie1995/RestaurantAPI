using Lab1.Data.Repos.IRepos;
using Lab1.Models;

namespace Lab1.Data.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly RestaurantContext _context;
        private readonly IConfiguration _configuration;

        public AdminRepo(RestaurantContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Admin> GetAdminByUsername(string userName)
        {
            var existingAdmin = _context.Admin.SingleOrDefault(a => a.UserName == userName);

            return existingAdmin;
        }

        public async Task RegisterAdminAsync(Admin admin)
        {
            await _context.Admin.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
    }
}
