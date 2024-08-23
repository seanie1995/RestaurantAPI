using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer, Customer newCustomer);
        Task DeleteCustomer(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);

    }
}
