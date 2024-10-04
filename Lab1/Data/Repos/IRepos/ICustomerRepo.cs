using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer, Customer newCustomer);
        Task<bool> DeleteCustomerById(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);
        Task<bool> AddBookingToCustomerAsync(Customer customer, Booking booking);
        Task<Customer> GetCustomerByEmailAsync(string email);



    }
}
