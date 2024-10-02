using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer, Customer newCustomer);
        Task DeleteCustomerById(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);
        Task AddBookingToCustomerAsync(Customer customer, Booking booking);
        Task<Customer> GetCustomerByEmailAsync(string email);



    }
}
