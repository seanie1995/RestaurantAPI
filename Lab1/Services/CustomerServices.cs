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
            // Validate the input before proceeding
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer data cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(customer.LastName) || string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Last Name and Email cannot be blank.", nameof(customer.LastName));
            }

            // If validation passes, proceed to create the Customer object
            var newCustomer = new Customer
            {
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Email = customer.Email,
            };

            // Save the new customer to the repository
            await _customerRepo.AddCustomerAsync(newCustomer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customerToDelete = await _customerRepo.GetCustomerByIdAsync(id);

            if (customerToDelete == null) 
            {
                throw new NotFoundException("User not found");
            }

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
            }); 
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
