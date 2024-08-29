using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Server.IIS;

namespace Lab1.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _bookingRepo;   
        private readonly ITableRepo _tableRepo;
        private readonly ICustomerRepo _customerRepo;
        public BookingServices(ITableRepo tableRepo ,IBookingRepo bookingRepo, ICustomerRepo customerRepo )
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
            _customerRepo = customerRepo;
        }

        public async Task AddBookingAsync(int customerId, BookingDTO booking)
        {
            var newCustomer = await _customerRepo.GetCustomerByIdAsync(customerId);
			
			var newBooking = new Booking
            {
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,              
                PartySize = booking.PartySize,
				FK_CustomerId = newCustomer.Id         
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

        public async Task<IEnumerable<Booking>> GetCustomerBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepo.GetCustomerBookingsByCustomerIdAsync(customerId);

            return bookings;
        }

        public async Task AddTableToBookingByIdAsync(int tableId, int bookingId)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId);
            var table = await _tableRepo.GetTableByIdAsync(tableId);


               
            await _bookingRepo.AddTableToBookingByIdAsync(table, booking);
        }
	}
}
