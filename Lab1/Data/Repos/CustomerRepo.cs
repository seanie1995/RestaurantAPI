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
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerById(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            } else
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
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

        public async Task<bool> UpdateCustomerAsync(Customer existingCustomer, Customer updateCustomer)
        {
            //var existingCustomer = await _context.Customer.FindAsync(customer.Id);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = updateCustomer.FirstName;
                existingCustomer.LastName = updateCustomer.LastName;
                existingCustomer.Email = updateCustomer.Email;
            }
            else
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(e => e.Email == email);

            return customer;
        }


		public async Task<bool> AddBookingToCustomerAsync(Customer customer, Booking booking)
		{
			if (customer.Bookings == null)
			{
				customer.Bookings = new List<Booking>();
			}
			customer.Bookings.Add(booking);
			return true;
		}
	}
}
