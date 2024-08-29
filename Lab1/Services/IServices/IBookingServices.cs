using Lab1.Models;
using Lab1.Models.DTOs;

namespace Lab1.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(int customerId,  BookingDTO booking);
        Task UpdateBooking(int id, BookingDTO updateBooking);
        Task DeleteBookingByIdAsync(int id);

        Task<IEnumerable<Booking>>GetCustomerBookingsByCustomerIdAsync(int customerId);

		Task AddTableToBookingByIdAsync(int tableId, int bookingId);
	}
}
