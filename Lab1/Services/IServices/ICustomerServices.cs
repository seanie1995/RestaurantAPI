using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;

namespace Lab1.Services.IServices
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();
        Task<CustomerViewModel> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerDTO customer);
        Task UpdateCustomerAsync(int id, CustomerDTO newCustomer);
        Task DeleteCustomerAsync(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);
    }
}
