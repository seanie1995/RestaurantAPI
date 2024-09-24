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

        public async Task UpdateBookingAsync(Booking booking, Booking updateBooking)
        {
            var existingBooking = await _context.Booking.FindAsync(booking.Id);

            if (existingBooking != null )
            {
                existingBooking.PartySize = updateBooking.PartySize;
                existingBooking.BookingStart = updateBooking.BookingStart;
                existingBooking.BookingEnd = updateBooking.BookingEnd;
                existingBooking.FK_TableId = updateBooking.FK_TableId;
            }
		
			await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetCustomerBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _context.Booking.Where(b => b.FK_CustomerId == customerId).ToListAsync();

            return bookings;
        }

		public async Task UpdateBookingTableAsync(Table table, Booking booking)
        {

            booking.FK_TableId = table.Id;
            booking.Table = table;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingPartySizeAsync(int partySize,Booking booking)
        {
            booking.PartySize = partySize;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingTimeAsync(Booking booking, DateTime bookingStart, DateTime bookingEnd)
        {
            booking.BookingStart = bookingStart;
            booking.BookingEnd = bookingEnd;

            await _context.SaveChangesAsync();
        }
	}
}
