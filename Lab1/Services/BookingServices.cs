using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Server.IIS;
using System.Runtime.InteropServices.Marshalling;

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

		// Method below takes a customerId and a bookingDTO. Customer is found and it's Id is automatically added to new booking
		public async Task AddBookingAsync(int customerId, BookingDTO booking) 
        {
            var newCustomer = await _customerRepo.GetCustomerByIdAsync(customerId);
		    
            if (newCustomer == null)
            {
                throw new Exception("Customer input is null");
            }

            var newBooking = new Booking
            {
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                PartySize = booking.PartySize,
                FK_CustomerId = newCustomer.Id,
            };

            await _bookingRepo.AddBookingAsync(newBooking);
           	    		
        }
        // Method below takes a booking ID, finds the booking with ID and deletes it. 
        public async Task DeleteBookingByIdAsync(int id)
        {          
            await _bookingRepo.DeleteBookingByIdAsync(id);
        }

        // Method below returns a list of BookingViewModels
        public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
        {
            var bookingList = await _bookingRepo.GetAllBookingsAsync();

            var bookingListViewModels = bookingList.Select(b => new BookingViewModel
            {
                Id = b.Id,
                PartySize = b.PartySize,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                CustomerId = b.FK_CustomerId,
				TableId = b.FK_TableId
			}).ToList();

            return bookingListViewModels;
        }
        // Method below takes a booking ID and returns booking with corresponding ID.
        public async Task<BookingViewModel> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(id);

            if (booking == null)
            {
                throw new Exception($"Booking with ID: {id} not found");
            }

            var bookingViewModel = new BookingViewModel
            {
                Id = booking.Id,
                PartySize = booking.PartySize,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                CustomerId = booking.FK_CustomerId,
                TableId = booking.FK_TableId
            };

            return bookingViewModel;
        }

        public async Task UpdateBookingAsync(int existingBookingId, BookingDTO updatedBooking)
        {
            var bookingToUpdate = await _bookingRepo.GetBookingByIdAsync(existingBookingId);

            var tableToCheck = await _tableRepo.GetTableByIdAsync(bookingToUpdate.FK_TableId.Value);

            var newBooking = new Booking
            {
                PartySize = updatedBooking.PartySize,
                BookingStart = updatedBooking.BookingStart,
                BookingEnd = updatedBooking.BookingEnd,
            };

			var hasSeats = await CheckIfTableHasEnoughSeatsAsync(tableToCheck.Id, updatedBooking.PartySize);
            var hasAvailableTime = await CheckIfTableIsAvailableAsync(tableToCheck.Id, bookingToUpdate.Id);

            if (!hasSeats)
            {
                throw new Exception("Table does not have enough seats");
            }
            if (!hasAvailableTime)
            {
                throw new Exception("Table is already booked for this time slot");
            }

            await _bookingRepo.UpdateBookingAsync(bookingToUpdate, newBooking);
            
        }

        public async Task<IEnumerable<BookingViewModel>> GetCustomerBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepo.GetCustomerBookingsByCustomerIdAsync(customerId);

            var bookingsViewModels = bookings.Select(static b => new BookingViewModel
            {
                Id = b.Id,
                PartySize = b.PartySize,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId
            }).ToList();

            return bookingsViewModels;
        }

        public async Task AddTableToBookingByIdAsync(int tableId, int bookingId)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(bookingId);
            var table = await _tableRepo.GetTableByIdAsync(tableId);
            
            if (!await CheckIfTableIsAvailableAsync(tableId, bookingId))
            {
                throw new Exception($"Table unavailable at this time slot");
            }

            if (!await CheckIfTableHasEnoughSeatsAsync(table.Id, booking.PartySize))
            {
                throw new Exception("Table does not have enough seats");
            }
            
            else
            {
				await _bookingRepo.AddTableToBookingByIdAsync(table, booking);
			}        
        }

		public async Task<bool> CheckIfTableIsAvailableAsync(int tableId, int bookingId)
		{
			var bookingsList = await _tableRepo.GetBookingsConnectedToTableByIdAsync(tableId);
			var bookingToCheck = await _bookingRepo.GetBookingByIdAsync(bookingId);

			if (bookingsList == null || bookingToCheck == null)
			{
				throw new Exception("Booking or booking list not found");
			}

			foreach (var booking in bookingsList)
			{
				// Ignore the current booking being updated
				if (booking.Id == bookingId)
				{
					continue;
				}

				// Check for overlap
				if ((booking.BookingStart == bookingToCheck.BookingStart && booking.BookingEnd == bookingToCheck.BookingEnd) ||
	                (booking.BookingStart <= bookingToCheck.BookingEnd && booking.BookingEnd >= bookingToCheck.BookingStart))
				{
					return false;
				}

			}
			return true;
		}


		public async Task<bool> CheckIfTableHasEnoughSeatsAsync(int tableId, int partySize)
        {
            var table = await _tableRepo.GetTableByIdAsync(tableId);
           
            return partySize <= table.Capacity;
        }
	}
}
