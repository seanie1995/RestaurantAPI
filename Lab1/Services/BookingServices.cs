using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;

namespace Lab1.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly ITableRepo _tableRepo;
        public BookingServices(ITableRepo tableRepo ,IBookingRepo bookingRepo, ICustomerRepo customerRepo)
        {
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            _tableRepo = tableRepo;
        }

        public async Task AddBookingAsync(int customerId, BookingDTO booking)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);

            var tablesWithEnoughSeats = await _tableRepo.FindTableWithEnoughSeatsAsync(booking.PartySize);

			var availableTable = tablesWithEnoughSeats.FirstOrDefault(table =>
		        !table.Bookings.Any(b =>
			    (booking.BookingStart < b.BookingEnd) &&
			    (booking.BookingEnd > b.BookingStart)));

            var newBooking = new Booking
            {
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                Customer = customer,
                PartySize = booking.PartySize,
                FK_CustomerId = customer.Id,
                Table = availableTable,
                FK_TableId = availableTable.Id
            };

            await _bookingRepo.AddBookingAsync(newBooking);
        }

        public async Task DeleteBookingByIdAsync(int id)
        {
            await _bookingRepo.DeleteBookingByIdAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingList = await _bookingRepo.GetAllBookingsAsync();

            return bookingList;
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(id);

            return booking;
        }

        public async Task UpdateBooking(int id, BookingDTO updateBooking)
        {
            var bookingToUpdate = await _bookingRepo.GetBookingByIdAsync(id);

            var newBooking = new Booking
            {
                BookingStart = updateBooking.BookingStart,
                BookingEnd = updateBooking.BookingEnd,
            };
            
        }
    }
}
