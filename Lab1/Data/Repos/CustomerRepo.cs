using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly RestaurantContext _context;

        public CustomerRepo(RestaurantContext context)
        {
            _context = context;
        }
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customerList = await _context.Customer.ToListAsync();
            return customerList;
        }

        public async Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            var bookingList = customer.Bookings.ToArray();

            return bookingList;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            return customer;
        }

        public async Task UpdateCustomer(Customer customer, Customer newCustomer)
        {
            var existingCustomer = await _context.Customer.FindAsync(customer.Id);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = newCustomer.FirstName;
                existingCustomer.LastName = newCustomer.LastName;
                existingCustomer.Email = newCustomer.Email;
            }

            await _context.SaveChangesAsync();


        }
    }
}
