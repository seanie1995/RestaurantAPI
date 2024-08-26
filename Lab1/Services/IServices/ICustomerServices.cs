using Lab1.Models;
using Lab1.Models.DTOs;

namespace Lab1.Services.IServices
{
    public interface ICustomerServices
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerDTO customer);
        Task UpdateCustomerAsync(int id, CustomerDTO newCustomer);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);
    }
}
