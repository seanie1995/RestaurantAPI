using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface IBookingRepo
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking, Booking updateBooking);
        Task<bool> DeleteBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetCustomerBookingsByCustomerIdAsync(int customerId);
        Task UpdateBookingTableAsync(Table table, Booking booking);
        Task UpdateBookingPartySizeAsync(int partySize, Booking booking);
        Task UpdateBookingTimeAsync(Booking booking, DateTime bookingStart, DateTime bookingEnd);   

    }
}
