using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
    public interface IBookingRepo
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBooking(Booking booking, Booking updateBooking);
        Task DeleteBookingByIdAsync(int id);
    }
}
