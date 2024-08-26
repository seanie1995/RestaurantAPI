using Lab1.Data.Repos.IRepos;
using Lab1.Exceptions;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;

namespace Lab1.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerServices(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task AddCustomerAsync(CustomerDTO customer)
        {
            var newCustomer = new Customer
            {
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Email = customer.Email,             
            };

            await _customerRepo.AddCustomerAsync(newCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customerToDelete = await _customerRepo.GetCustomerByIdAsync(id);

            await _customerRepo.DeleteCustomerById(customerToDelete.Id);
        }

        public Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customerList = await _customerRepo.GetAllCustomersAsync();

            return customerList.Select(u => new Customer
            {
                Id = u.Id,
                LastName = u.LastName,
                FirstName = u.FirstName,
                Email = u.Email
            }); ;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);

            if (customer != null)
            {
                return customer;
            }
            else
            {
                throw new NotFoundException($"Customer with {id} not found.");   
            }

        }

        public async Task UpdateCustomerAsync(int id, CustomerDTO newCustomer)
        {
            var customerToUpdate = await _customerRepo.GetCustomerByIdAsync(id);

            var updatedCustomer = new Customer
            {
                LastName = newCustomer.LastName,
                FirstName = newCustomer.FirstName,
                Email = newCustomer.Email
            };

            await _customerRepo.UpdateCustomerAsync(customerToUpdate, updatedCustomer);
        }
    }
}
