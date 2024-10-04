using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Result;

namespace Lab1.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync();
        Task<BookingViewModel> GetBookingByIdAsync(int id);
        Task<ServiceResult> AddBookingAsync(int customerId,  BookingDTO booking);
        Task<ServiceResult> UpdateBookingAsync(int id, BookingDTO updateBooking);
        Task<ServiceResult> DeleteBookingByIdAsync(int id);
        Task<IEnumerable<BookingViewModel>>GetCustomerBookingsByCustomerIdAsync(int customerId);
		
        Task<bool> CheckIfTableIsAvailableAsync(int tableId, DateTime bookingStart, DateTime bookingEnd);
        Task<bool> CheckIfTableHasEnoughSeatsAsync(int tableId, int partySize);

        Task AddBookingByCustomer(string email, BookingDTO booking);
	}
}
