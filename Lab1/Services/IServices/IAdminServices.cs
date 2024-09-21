namespace Lab1.Services.IServices
{
    public interface IAdminServices
    {
        Task<string> AuthenticateAdminAsync(string username, string password);
    }
}
