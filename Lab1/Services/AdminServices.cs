using Lab1.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace Lab1.Services
{
    public class AdminServices : IAdminServices
    {

        private readonly string _secretKey;

        public AdminServices(IConfiguration config)
        {
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        }

        public Task<string> AuthenticateAdminAsync(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                throw new Exception("Not Finished");
            }
            throw new Exception("Not Finished");
        }

    }
}
