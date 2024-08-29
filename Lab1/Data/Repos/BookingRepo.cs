using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data.Repos
{
    public class BookingRepo : IBookingRepo
    {
        private readonly RestaurantContext _context;

        public BookingRepo(RestaurantContext context)
        {
            _context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingByIdAsync(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            if (booking != null)
            {
                _context.Remove(booking);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingList = await _context.Booking.ToListAsync();
            return bookingList;
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            return booking;
        }

        public async Task UpdateBooking(Booking booking, Booking updateBooking)
        {
            var existingBooking = await _context.Booking.FindAsync(booking.Id);

            if (existingBooking != null)
            {
                existingBooking.BookingStart = updateBooking.BookingStart;
                existingBooking.BookingEnd = updateBooking.BookingEnd;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _context.Booking.Where(b => b.FK_CustomerId == customerId).ToListAsync();

            return bookings;
        }

		public async Task AddTableToBookingByIdAsync(Table table, Booking booking)
        {

            booking.FK_TableId = table.Id;
            booking.Table = table;

            await _context.SaveChangesAsync();
        }
	}
}
